ns('SoftwareNet.DJ.Web.Components');
SoftwareNet.DJ.Web.Components.Dialog = function (opts) {
    this.init(opts);
};

SoftwareNet.DJ.Web.Components.Dialog.prototype = {
    idDivDialog: 'divDialog',
    noCloseButtonClass: 'no-close-dialog',
    init: function (opts) {
        this._privateFunction.createDialog.apply(this, [opts]);
    },

    setCloseOnEscape: function (value) {
        if (this.divDialog) {
            this.divDialog.progressbar('option', 'closeOnEscape', value);
        }
    },

    setCloseText: function (value) {
        if (this.divDialog) {
            this.divDialog.dialog('option', 'closeText', value);
        }
    },

    setDraggable: function (value) {
        if (this.divDialog) {
            this.divDialog.dialog('option', 'draggable', value);
        }
    },

    setHeight: function (value) {
        if (this.divDialog) {
            this.divDialog.dialog('option', 'height', value);
        }
    },

    setWidth: function (value) {
        if (this.divDialog) {
            this.divDialog.dialog('option', 'width', value);
        }
    },

    setMaxHeight: function (value) {
        if (this.divDialog) {
            this.divDialog.dialog('option', 'maxHeight', value);
        }
    },

    setMaxWidth: function (value) {
        if (this.divDialog) {
            this.divDialog.dialog('option', 'maxWidth', value);
        }
    },

    setMinHeight: function (value) {
        if (this.divDialog) {
            this.divDialog.dialog('option', 'minHeight', value);
        }
    },

    setMinWidth: function (value) {
        if (this.divDialog) {
            this.divDialog.dialog('option', 'minWidth', value);
        }
    },

    setModal: function (value) {
        if (this.divDialog) {
            this.divDialog.dialog('option', 'modal', value);
        }
    },

    setPosition: function (value) {
        if (this.divDialog) {
            this.divDialog.dialog('option', 'position', value);
        }
    },

    setResizable: function (value) {
        if (this.divDialog) {
            this.divDialog.dialog('option', 'resizable', value);
        }
    },

    setTitle: function (value) {
        if (this.divDialog) {
            this.divDialog.dialog('option', 'title', value);
        }
    },

    setClass: function (value) {
        if (this.divDialog) {
            this.divDialog.dialog('option', 'dialogClass', value);
        }
    },

    setButtons: function (value) {
        if (this.divDialog) {
            this.divDialog.dialog('option', 'buttons', value);
        }
    },
    //nueva opcion
    setFluid: function (value) {
        if (this.divDialog) {
            this.divDialog.dialog('option', 'fluid', value);
        }
    },
    getMainContainer: function () {
        return this.divDialog;
    },

    getHeight: function (value) {
        var value = undefined;
        if (this.divDialog) {
            value = this.divDialog.dialog('option', 'height');
        }
        return value;
    },

    getWidth: function (value) {
        var value = undefined;
        if (this.divDialog) {
            value = this.divDialog.dialog('option', 'width');
        }
        return value;
    },

    getPosition: function (value) {
        var value = undefined;
        if (this.divDialog) {
            value = this.divDialog.dialog('option', 'position');
        }
        return value;
    },

    getTitle: function (value) {
        var value = undefined;
        if (this.divDialog) {
            value = this.divDialog.dialog('option', 'title');
        }
        return value;
    },

    isModal: function (value) {
        var value = undefined;
        if (this.divDialog) {
            value = this.divDialog.dialog('option', 'modal');
        }
        return value;
    },

    isResizable: function (value) {
        var value = undefined;
        if (this.divDialog) {
            value = this.divDialog.dialog('option', 'resizable');
        }
        return value;
    },

    isOpen: function (value) {
        var value = undefined;
        if (this.divDialog) {
            value = this.divDialog.dialog('isOpen');
        }
        return value;
    },

    close: function () {
        if (this.divDialog && this.isOpen()) {
            this.divDialog.dialog('close');
        }
    },

    open: function () {
        if (this.divDialog && !this.isOpen()) {
            if (navigator.userAgent.indexOf('Chrome') != -1) {
                $(window).scrollTop(0);
                $("body").scrollTop(0);
            }

            this.divDialog.dialog('open');
            $("body").css("overflow", "hidden");
            $('.ui-dialog-titlebar-close').prop('title', '');
            $('.ui-dialog').css('overflow', 'visible');
            // onClosed: function(){ $("body").css("overflow", "auto"); }
        }
    },

    moveToTop: function () {
        if (this.divDialog) {
            this.divDialog.dialog('moveToTop');
        }
    },

    setContent: function (html) {
        if (this.divDialog) {
            this.divDialog.html(html);
        }
    },

    setElement: function (element1, element2) {
        if (this.divDialog) {
            this.divDialog.empty();

            this.divDialog.append(element1);
            this.divDialog.append(element2);
        }
    },
    getAjaxContent: function (opts) {
        var me = this;
        var ajaxBuscar = new SoftwareNet.DJ.Web.Components.Dialog.Ajax({
            action: opts.action,
            dataType: 'html',
            data: opts.data,
            onSuccess: function (html) {
                me.setContent(html);
                SoftwareNet.DJ.Web.Components.TextBox.Function.Configure(me.idDivDialog);

                if (opts.onSuccess) {
                    opts.onSuccess.apply(opts.scope ? opts.scope : me, [html, opts.customParam]);
                }

                opts.autoOpen = (opts.autoOpen == undefined || opts.autoOpen == null) ? true : opts.autoOpen;

                if (opts.autoOpen) {
                    me.open();
                    if (opts.afterOpen) {
                        opts.afterOpen.apply(opts.scope ? opts.scope : me, [html, opts.customParam]);
                    }
                }

                if (QuitarDrop) {
                    QuitarDrop();
                }
            }
        });
    },

    destroy: function () {
        if (this.divDialog) {
            this.divDialog.dialog('destroy');
            this.divDialog.remove();
        }
    },

    _privateFunction: {
        createDialog: function (opts) {
            if (!opts.target || opts.target == '') {
                this.divDialog = this._privateFunction.implementDialogElement.apply(this, [opts]);
            }
            else {
                this.divDialog = $('#' + opts.target);
            }

            this.divDialog.dialog(this._privateFunction.getConfig.apply(this, [opts]));
        },

        implementDialogElement: function (opts) {
            var div = $('<div />').uniqueId();
            this.idDivDialog = this.idDivDialog + div.attr('id');
            div.attr('id', this.idDivDialog);
            div.attr('class', 'text-center');
            $('body').append(div);
            return div;
        },

        getConfig: function (opts) {
            var config = {
                autoOpen: opts.autoOpen ? opts.autoOpen : false,
                modal: opts.modal ? opts.modal : true,
                resizable: opts.resizable ? opts.resizable : false,
                show: { effect: "clip", duration: 400 },
                hide: { effect: "clip", duration: 50 }
                , close: function () { $("body").css("overflow", "auto"); }
            };

            if (opts) {
                for (var property in opts) {
                    config[property] = opts[property];
                }

                if (opts.showCloseButton == false) {
                    config.dialogClass = config.dialogClass ? config.dialogClass + ' ' + this.noCloseButtonClass : this.noCloseButtonClass;
                }

                if (opts.close && opts.close != null) {
                    config.close = function () {
                        $("body").css("overflow", "auto");
                        opts.close();
                    };
                }
            }

            return config;
        }
    }
};