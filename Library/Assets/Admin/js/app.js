var queryStrings = {};
var body;

function pushNotify(msg, type) {
    //success, info, warn, error
    $.notify(msg, type == null ? 'info' : type);
}

//các tham số của button
//label: Tên hiển thị
//cssClass: css Class
//hotkey: phim tắt
//action: event
var dialogs = {
    getType: function (type) {
        switch (type) {
            case "primary":
                return BootstrapDialog.TYPE_PRIMARY;
            case "info":
                return BootstrapDialog.TYPE_INFO;
            case "success":
                return BootstrapDialog.TYPE_SUCCESS;
            case "warning":
                return BootstrapDialog.TYPE_WARNING;
            case "error":
            case "danger":
                return BootstrapDialog.TYPE_DANGER;
        }
        return BootstrapDialog.TYPE_DEFAULT;
    },
    getTitle: function (title) {
        return title == null || title.trim().length == 0 ? 'Thông báo' : title;
    },
    getSize: function (size) {
        switch (size) {
            case "small":
                return BootstrapDialog.SIZE_SMALL;
            case "wide":
                return BootstrapDialog.SIZE_WIDE;
            case "large":
                return BootstrapDialog.SIZE_LARGE;
        }
        return BootstrapDialog.SIZE_NORMAL;
    },
    alert: function (options) {
        if (options.buttons == null)
            options.buttons = [
                {
                    label: 'Đóng', cssClass: 'btn-default',
                    hotkey: 13,
                    action: function (dialog) {
                        dialog.close();

                        if (options.buttonclick != null)
                            options.buttonclick(dialog, 0);
                    }
                }
            ];

        BootstrapDialog.show({
            title: this.getTitle(options.title),
            type: this.getType(options.type),
            size: this.getSize(options.size),
            closable: options.closable == null ? true : options.closable,
            closeByBackdrop: options.closeByBackdrop == null ? false : options.closeByBackdrop,
            closeByKeyboard: options.closeByKeyboard == null ? true : options.closeByKeyboard,
            message: options.msg,
            buttons: options.buttons
        });
    },
    confirm: function (options) {
        if (options.buttons == null) options.buttons = [{ label: 'Đồng ý', cssClass: (options.type != null && options.type.length > 0 ? ('btn-' + options.type) : 'btn-primary'), hotkey: 13 }, { label: 'Hủy bỏ', cssClass: 'btn-default' }];

        if (options.buttonclick == null) options.buttonclick = function (dialog) { dialog.close(); };

        $.each(options.buttons, function (index) { this.action = function (dialog) { options.buttonclick(dialog, index); }; });

        BootstrapDialog.show({
            title: this.getTitle(options.title),
            type: this.getType(options.type),
            size: this.getSize(options.size),
            closable: options.closable == null ? true : options.closable,
            closeByBackdrop: options.closeByBackdrop == null ? false : options.closeByBackdrop,
            closeByKeyboard: options.closeByKeyboard == null ? true : options.closeByKeyboard,
            message: options.msg,
            buttons: options.buttons
        });
    },
    load: function (options) {
        if (options.buttons == null) options.buttons = [{ label: 'Đóng', cssClass: 'btn-default' }];

        if (options.buttonclick == null) options.buttonclick = function (dialog) { dialog.close(); };

        $.each(options.buttons, function (index) { this.action = function (dialog) { options.buttonclick(dialog, index); }; });

        BootstrapDialog.show({
            title: this.getTitle(options.title),
            type: this.getType(options.type),
            size: this.getSize(options.size),
            closable: options.closable == null ? true : options.closable,
            closeByBackdrop: options.closeByBackdrop == null ? false : options.closeByBackdrop,
            closeByKeyboard: options.closeByKeyboard == null ? true : options.closeByKeyboard,
            message: $('<div></div>').load(options.url, function () {
                var container = $(this);
                initPlugins(container);
                if (options.onshow != null)
                    options.onshow(container);
            }),
            onshown: function () {
                var container = $("#" + this.id);
                initPlugins(container);
                if (options.onshow != null)
                    options.onshow(container);
            },
            buttons: options.buttons,
            onhidden: function () {
                destroyPlugins($("#" + this.id));
            }
        });
    },
    getInstance: function (id) {
        if (typeof id === "object")
            id = $(id).closest(".bootstrap-dialog").attr("id");
        return BootstrapDialog.getDialog(id);
    }
}

function initPlugins(container) {
    if (container == null)
        container = $('body');

    if (container == null)
        return;

    if (typeof $.fn.iCheck === "function") {
        container.find('input[type="checkbox"]:not(.styled):visible, input[type="radio"]:not(.styled):visible').addClass("styled").iCheck({
            checkboxClass: 'icheckbox_minimal-blue',
            radioClass: 'iradio_minimal-blue'
        });
    }

    if (typeof $.fn.select2 === "function") {
        container.find('select:not(.styled)').each(function () {
            var select = $(this);
            select.addClass("styled");

            var placeholder = select.data('placeholder');
            var allowclear = select.data('allowclear');

            var options = {
                minimumInputLength: 0,
                width: '100%',
                //width: 'resolve',
                dropdownParent: select.parent(),
                placeholder: placeholder == null ? '' : placeholder,
                allowClear: allowclear != null && allowclear == true
            }
            var dataWidth = select.data('width');
            if (isNull(dataWidth))
                options.width = '100%';
            else
                options.width = dataWidth;


            var datasourceUrl = select.data("datasource-url");
            if (datasourceUrl != null && datasourceUrl.length > 0) {
                var isFunction = typeof window[datasourceUrl] === 'function';
                options.ajax = {
                    delay: 300,
                    url: function () {
                        return isFunction ? window[datasourceUrl]() : datasourceUrl;
                    },
                    dataType: 'json',
                    data: function (params) {
                        return {
                            q: params.term,
                            page: params.page
                        };
                    },
                    processResults: function (data, params) {
                        params.page = params.page || 1;

                        return {
                            results: data.items,
                            pagination: {
                                more: (params.page * 10) < data.total_count
                            }
                        };
                    }
                };
            }

            if (select.data('treeview') != null) {
                options.templateResult = function(option) {
                    var text = option.text;
                    var index = text.indexOf(',');
                    var level = text.substring(0, index);

                    index = text.indexOf(':');
                    var parent = level.substring(index + 1);
                    level = level.substring(0, index);

                    return parent === '1' ? $('<div style="margin-left: ' + (level * 15 + 12) + 'px" class="parent"><img src="/Images/arrow-left-down.png" class="arrow"/> ' + text.substring((level + ":" + parent).length + 1) + '</div>') : $('<div style="padding-left: ' + (level * 15 + 12) + 'px">' + text.substring((level + ":" + parent).length + 1) + '</div>');
                };
                options.templateSelection = function(option) {
                    var text = option.text;
                    var index = text.indexOf(',');
                    return $('<span>' + text.substring(index + 1) + '</span>');
                };
            }

            select.select2(options);
        });
    }

    if (typeof $.fn.datepicker === "function") {
        var input = container.find('.datepicker:not(.styled)');
        input.prop('readonly', false);
        input.inputmask('dd/mm/yyyy', { 'placeholder': 'dd/mm/yyyy' });
        input.addClass("styled").datepicker({
            autoclose: true,
            format: 'dd/mm/yyyy',
            showOnFocus: true,
            todayHighlight: true,
            orientation: "bottom"
        });
    }

    if (typeof $.fn.timepicker === "function") {
        container.find('.timepicker:not(.styled)').addClass("styled").timepicker({
            showMeridian: false,
            showInputs: false
        });
    }

    if (typeof $.validator === 'function') {
        $.validator.unobtrusive.parse('form');
    }

    if (typeof CKEDITOR !== 'undefined') {
        if (typeof CKFinder !== 'undefined')
            CKFinder.setupCKEditor(null, '/ckfinder');

        container.find('.html-editor').each(function () {
	        CKEDITOR.replace(this.name);
        });
    }

    if (typeof $.fn.inputmask === "function") {
        container.find('.timerpicker:not(.styled)').each(function () {
            var input = $(this);
            input.addClass('styled');
            input.inputmask('hh:mm', { 'placeholder': 'hh:mm' });
        });

        container.find('input[data-mask]:not(.styled)').each(function () {
            var input = $(this);
            input.addClass('styled');
            input.inputmask(input.data('mask'), { 'placeholder': input.data('mask') });
        });
    }
}

function destroyPlugins(container) {
    if (container == null)
        container = $('body');

    if (container == null)
        return;

    if (typeof $.fn.iCheck === "function") {
        container.find('input[type="checkbox"].styled, input[type="radio"].styled').removeClass("styled").iCheck('destroy');
    }

    //if (typeof $.fn.select2 === "function") {
    //    container.find('select.styled').removeClass("styled").select2('destroy');
    //}

    if (typeof $.fn.datepicker === "function") {
        container.find('.datepicker.styled').removeClass("styled").datepicker("destroy");
    }
}

function submitFormByAjax(form) {
    var jform = $(form);

    if (jform.data("state") == "requesting")
        return false;

    jform.data("state", "requesting");
    var btnSubmits = jform.find('button:not([type="submit"]), input[type="submit"]');
    btnSubmits.prop("disabled", true);

    var url = jform.attr("action");
    if (url == null || url.trim().length == 0)
        url = location.pathname;

    var method = jform.attr("method");
    if (method == null || method.trim().length == 0)
        method = "GET";

    var data = new FormData();
    var formData = jform.serializeArray();
    $.each(formData, function () {
        var jinput = jform.find('[name="' + this.name + '"]');
        if (jinput.length > 0 && jinput.hasClass('html-editor') === true)
            this.value = CKEDITOR.instances[this.name].getData();
	    data.append(this.name, this.value);
    });

    var inputFiles = jform.find('input[type="file"]');
    $.each(inputFiles, function () {
        var inputFile = $(this);
        var name = inputFile.attr("name");
        if (name == null || name.length == 0)
            return;

        $.each(this.files, function () {
            data.append(name, this);
        });
    });

    $.ajax({
        url: url,
        data: data,
        dataType: 'json',
        contentType: false,
        processData: false,
        cache: false,
        type: method.toUpperCase(),
        success: function (response) {
            btnSubmits.prop("disabled", false);
            jform.removeData("state");

            pushNotify(response.msg, response.status == 0 ? 'success' : 'error');

            if (response.errorField != null) {
                var jinput = jform.find('input[type="text"][name="' + response.errorField + '"],input[type="password"][name="' + response.errorField + '"],input[type="tel"][name="' + response.errorField + '"],input[type="number"][name="' + response.errorField + '"],input[type="email"][name="' + response.errorField + '"],textarea[name="' + response.errorField + '"],select[name="' + response.errorField + '"]');
                if (jinput.length > 0) {
                    jinput.val('');
                    jinput.focus();
                }
            }

            var fnSuccess = jform.data("submitsuccess");
            if (fnSuccess == null || typeof window[fnSuccess] !== "function") return;
            window[fnSuccess](form, response);
        },
        error: function () {
            btnSubmits.prop("disabled", false);
            jform.removeData("state");

            var fnFail = jform.data("submitfail");
            if (fnFail == null || typeof window[fnFail] !== "function") return;
            window[fnFail]();
        }
    });

    return false;
}

function onClick_btnLogout() {
    dialogs.confirm({
        msg: 'Bạn có thật sự muốn đăng xuất tài khoản khỏi hệ thống?',
        type: 'warning',
        buttonclick: function (dialog, buttonIndex) {
            dialog.close();
            if (buttonIndex == 0)
                location.href = '/AdminCP/Home/Logout';
        }
    });
}

function onClick_ChangePassword() {
    dialogs.load({
        title: 'Đổi mật khẩu',
        url: '/AdminCP/Home/ChangePassword',
        type: 'primary',
        buttons: [{ label: 'Lưu thay đổi', hotkey: 13, cssClass: 'btn-primary' }, { label: 'Hủy bỏ', cssClass: 'btn-default' }],
        buttonclick: function (dialog, buttonIndex) {
            if (buttonIndex == 1) {
                dialog.close();
                return;
            }
            $("#frmChangePassword").submit();
        }
    });
}

function onSubmitSuccess_frmChangePassword(form, response) {
    if (response.status == 0) {
        var dialog = dialogs.getInstance(form);
        if (dialog != null)
            dialog.close();
    }
}

$(document).ready(function () {
    var url = location.search;
    var qs = url.substring(url.indexOf('?') + 1).split('&');
    for (var i = 0; i < qs.length; i++) {
        qs[i] = qs[i].split('=');
        queryStrings[qs[i][0]] = decodeURIComponent(qs[i][1]);
    }

    if (queryStrings.notifyType != null && queryStrings.notifyType.length > 0 && queryStrings.notifyMsg != null && queryStrings.notifyMsg.length > 0)
        pushNotify(queryStrings.notifyMsg, queryStrings.notifyType);

    if (typeof $.fn.dataTable === "function") {
        $.extend($.fn.dataTable.defaults, {
            responsive: true,
            autoWidth: false,
            language: {
                "sProcessing": "Đang xử lý...",
                "sLengthMenu": "Xem _MENU_ mục",
                "sZeroRecords": "Không tìm thấy dòng nào phù hợp",
                "sInfo": "Đang xem _START_ đến _END_ trong tổng số _TOTAL_ mục",
                "sInfoEmpty": "Đang xem 0 đến 0 trong tổng số 0 mục",
                "sInfoFiltered": "(được lọc từ _MAX_ mục)",
                "sInfoPostFix": "",
                "sSearch": "Tìm:",
                "sUrl": "",
                "oPaginate": {
                    "sFirst": "Đầu",
                    "sPrevious": "Trước",
                    "sNext": "Tiếp",
                    "sLast": "Cuối"
                }
            },
            drawCallback: function () {
                var jtable = $(this);
                var jcheckall = jtable.find(".selector-all");
                if (jcheckall.length > 0)
                    jcheckall.prop("checked", false);
                initPlugins(jtable);
            },
            initComplete: function () {
                var jtable = $(this);
                var jcheckbox;
                var jtableScroll = jtable.closest('.dataTables_scroll');
                if (jtableScroll.length == 0) {
                    jcheckbox = jtable.find('.selector-all');
                    if (jcheckbox.length > 0)
                        jcheckbox.on('ifChanged', function() {
                            jtable.find(".selector").iCheck($(this).is(":checked") == true ? "check" : "uncheck");
                        });
                } else {
                    jcheckbox = jtableScroll.find('.selector-all');
                    if (jcheckbox.length > 0)
                        jcheckbox.on('ifChanged', function () {
                            jtableScroll.find(".selector").iCheck($(this).is(":checked") == true ? "check" : "uncheck");
                        });
                }
            }
        });
    }

    initPlugins();
});

body = $("body");
$(document).on({
    ajaxStart: function (e) {
        if (body.data('loading') === 'off')
            return;

        if (e.delegateTarget != null && e.delegateTarget.activeElement != null) {
            var element = $(e.delegateTarget.activeElement);
            var data = element.data('val-remote-url');
            if (data != null && data.trim().length > 0)
                return;

            if (element.closest('.dataTables_wrapper').length > 0)
                return;

            var cssClass = element.attr("class");
            if (cssClass != null && cssClass.indexOf("select2") !== -1)
                return;
        }
        body.addClass("loading");
    },
    ajaxStop: function () {        
        if (body.data('loading') === 'off') 
            body.data('loading', 'on');
        

        body.removeClass("loading");
    },
    ajaxError: function (e) {
        if (body.data('loading') === 'off')
            return;

        body.removeClass("loading");
        pushNotify('Lỗi!!! Không kết nối được đến máy chủ. Hãy kiểm tra lại kết nối Internet trên thiết bị hiện tại.', 'error');
    }
});

function openFileFinder(selector) {
	var finder = new CKFinder();
    finder.selectActionFunction = function (fileUrl) {
		$(selector).val(fileUrl);
	};
	finder.popup();
}

function convertToAlias(str) {
	str = str.replace(/^\s+|\s+$/g, ''); // trim
	str = str.toLowerCase();

	// remove accents, swap ñ for n, etc
	var from = 'áàảãạäâầấậẫẩăằắẵẳặèéẹẽẻëêềếệễểòóọõỏöôồốộỗổơờớởỡợìíỉĩịïîỳýỵỷỹuùúủũụüûưừứựữửñçđ·/_,:;';
	var to = 'aaaaaaaaaaaaaaaaaaeeeeeeeeeeeeooooooooooooooooooiiiiiiiyyyyyuuuuuuuuuuuuuuncd------';
	for (var i = 0, l = from.length; i < l; i++) {
		str = str.replace(new RegExp(from.charAt(i), 'g'), to.charAt(i));
	}

	str = str.replace(/[^a-z0-9 -]/g, '') // remove invalid chars
		.replace(/\s+/g, '-') // collapse whitespace and replace by -
		.replace(/-+/g, '-'); // collapse dashes

	return str;
}

function setAlias(s, d) {
    $(d).val(convertToAlias($(s).val()));
}
