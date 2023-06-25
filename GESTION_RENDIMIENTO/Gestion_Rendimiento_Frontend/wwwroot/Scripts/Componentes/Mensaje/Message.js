ns('SoftwareNet.DJ.Web.Components');
ns('SoftwareNet.DJ.Web.Shared.General.Resources');
SoftwareNet.DJ.Web.Components.Message = function (opts) {
    this.init(opts);
};

SoftwareNet.DJ.Web.Components.Message.prototype = {
    idDivDialogTitleBar: '.ui-dialog-titlebar',
    init: function (opts) {
        this._privateFunction.createMessage.apply(this, [opts]);
        ResultConfirm = false;
    },
    Controls: {
        InformationClose: function () { return $('#softwareNet-modal-information-close') },
        Dialog: function () { return $('#softwareNet-modal-information-close') },

        ModalInformation: function () { return $('#modalInformation') },
        ModalContentInformation: function () { return $('#contentModalInformation') },

        ModalWarning: function () { return $('#modalWarning') },
        ModalContentWarning: function () { return $('#contentModalWarning') },
        WarningClose: function () { return $('#softwareNet-modal-warning-close') },

        ModalConfirm: function () { return $('#modalConfirm') },
        ModalContentConfirm: function () { return $('#contentModalConfirm') },
        ConfirmAcept: function () { return $('#softwareNet-modal-confirm-acept') },
        ConfirmClose: function () { return $('#softwareNet-modal-confirm-cancel') }

    },
    Information: function (opts) {
        var me = SoftwareNet.DJ.Web.Components.Message.prototype;

        html = '<div id="modalInformation" tabindex="-1" class="modal fade in" data-backdrop="static" data-keyboard="false" style="z-index: 10000000 !important;display: block; padding-right: 17px;">';
        //html += '<div class="modal-dialog modal-sm">';
        html += '<div class="modal-dialog">';
        html += '<div class="modal-content">';
        html += '<div class="modal-body">';

        html += '<div class="modal-header">';
        html += '<h4>' + (!opts.title ? "" : opts.title) + '</h4>'
        html += '</div>';

        html += '<div class="text-center">';
        html += '<span class="text-success icon icon-check icon-5x"></span>';

        html += '<h3 class="text-success">' + (!opts.message ? "" : opts.message) + '</h3>';
        if (opts.codePrinter) {
            html += '<h3 id="divNumeroSolicitud">' +
                    '<span class="text-dark">N° Registro:&nbsp;</span><span class="text-primary">' + opts.codePrinter + '' +
                    '</span>' +
                '</h3>';
        }
        html += '<div class="m-t-lg">';
        html += '<button class="btn btn-default" id="softwareNet-modal-information-close" type="button">Cerrar</button>';
        html += '</div>';

        html += '</div>';
        html += '</div>';
        html += '</div>';
        html += '</div>';
        me.Controls.ModalContentInformation().html(html);
        me.Controls.ModalInformation().modal();
        me.Controls.ModalInformation().modal('show');
        eventInt = {
            InformationClose: function (opts) {
                var me = SoftwareNet.DJ.Web.Components.Message.prototype;
                me.Controls.InformationClose().on("click", function () {
                    me.Controls.ModalInformation().modal('hide');
                    $("body").addClass("layout layout-header-fixed modal-open");
                    if (opts)
                        opts.callback(true);
                });
            }
        }
        return eventInt;
    },
    InformationClose: function (opts) {
        var me = SoftwareNet.DJ.Web.Components.Message.prototype;
        me.Controls.InformationClose().on("click", function () {
            me.Controls.ModalInformation().modal('hide');
            //$('.modal.in').modal('hide');
            opts.callback(true);
        });
    },
    //Informations: function (opts) {       
    //    var me = this; $(this.idDivDialogTitleBar).hide();
    //    opts.dialogClass = 'message-dialog-information';
    //    opts.position = { my: "right top", at: "right top", of: window };
    //    opts.title = opts.title ? opts.title : SoftwareNet.DJ.Web.Shared.General.Resources.EtiquetaInformacion;
    //    opts.title = '<strong>' + opts.title + ' : </strong>'
    //    //opts.message = opts.title + opts.message; debugger;
    //    //opts.message = '<div class="alert alert-success">' + opts.message + '</div>';
    //    opts.messageContent = '<div class="text-center">' +
    //    '<span id="iconoRespuestaGuardar" class="text-success icon icon-check icon-5x"></span>';
    //    opts.messageContent += '<h3 class="text-success">' + opts.message + '</h3>';
    //    if (opts.codePrinter) {
    //        opts.messageContent += '<h3 id="divNumeroSolicitud">' +
    //                '<span class="text-dark">N° Registro:&nbsp;</span><span class="text-primary">' + opts.codePrinter + '' +
    //                '</span>' +
    //            '</h3>';
    //    }
    //    opts.messageContent += '<div class="m-t-lg">' +
    //    '<button class="btn btn-default" id="softwareNet-modal-information-close" type="button">Cerrar</button>' +
    //'</div>' +
    //'</div>';
    //    opts.message = opts.messageContent;
    //    opts.responseCode = null;
    //    opts.modal = false;
    //    opts.minWidth = 550;
    //    opts.minHeight = 60;
    //    this._privateFunction.show.apply(this, [opts]);
    //    //if (this.divDialog) {
    //    //    var me = this;
    //    //    this.informationTimeOut = setTimeout(function () {
    //    //        me.divDialog.close();
    //    //        //}, 2500);
    //    //    }, 2500);

    //    //}

    //},
    //InformationClose: function (opts) {
    //    var me = SoftwareNet.DJ.Web.Components.Message.prototype; 
    //    me.Controls.InformationClose().on("click", function () {
    //        debugger;
    //        //me.Controls.divDialog.close();
    //        me.Controls.ModalInformation().modal('hide');
    //        //me.Controls.ModalInformation().remove();
    //        opts.callback(true);
    //    });
    //},
    InformationCustom: function (opts) {
        var me = this;
        opts.dialogClass = 'message-dialog-informationCustom';
        opts.position = { my: "right top", at: "right top", of: window };
        opts.title = opts.title ? opts.title : SoftwareNet.DJ.Web.Shared.General.Resources.EtiquetaInformacion;
        opts.title = '<strong>' + opts.title + ' : </strong>'
        opts.message = opts.title + opts.message;
        opts.message = '<div class="alert alert-success">' + opts.message + '</div>';
        opts.modal = true;
        opts.minWidth = 550;
        opts.minHeight = 60;

        this._privateFunction.show.apply(this, [opts]);
    },

    ResultConfirm: false,

    ConfirmationCustom: function (opts) {
        var me = this;
        opts.dialogClass = 'message-dialog-confirmation';
        opts.fluid = true;

        var spanIcon = $('<span />');
        spanIcon.attr('class', 'text-primary icon icon-question-circle icon-5x');
        opts.spanIcon = spanIcon;

        var h3Title = $('<h3 />').text(opts.message);
        h3Title.attr('id', 'divDialogMessage');
        opts.h3Title = h3Title;

        opts.title = opts.title ? opts.title : SoftwareNet.DJ.Web.Shared.General.Resources.EtiquetaConfirmacion;
        opts.buttons = [
                            {
                                text: opts.textConfirmar ? opts.textConfirmar : SoftwareNet.DJ.Web.Shared.General.Resources.EtiquetaAceptarConfirmacion,
                                'class': 'ui-button-Confirmar',
                                click: function () {
                                    ResultConfirm = true;
                                    if (me.divDialog) {
                                        me.divDialog.close();
                                    }
                                    setTimeout(function () {
                                        if (opts.onConfirm) {
                                            opts.onConfirm(true);
                                        }
                                        if (opts.onAccept) {
                                            opts.onAccept();
                                        }
                                    }, 100);
                                }
                            },
                            {
                                text: opts.textCancelar ? opts.textCancelar : SoftwareNet.DJ.Web.Shared.General.Resources.EtiquetaCancelarConfirmacion,
                                'class': 'ui-button-Cancelar',
                                click: function () {
                                    if (me.divDialog) {
                                        me.divDialog.close();
                                    }
                                    setTimeout(function () {
                                        if (opts.onConfirm) {
                                            opts.onConfirm(false);
                                        }
                                        if (opts.onCancel) {
                                            opts.onCancel();
                                        }
                                    }, 100);

                                }
                            }
        ];
        this._privateFunction.show.apply(this, [opts]);
    },

    Confirmation: function (opts) {
        var me = SoftwareNet.DJ.Web.Components.Message.prototype;
        html = '<div id="modalConfirm" tabindex="-1" class="modal fade in" data-backdrop="static" data-keyboard="false" style="z-index: 10000000 !important;display: block; padding-right: 17px;">';
        html += '<div class="modal-dialog">';
        html += '<div class="modal-content">';
        html += '<div class="modal-body">';
        html += '<div class="text-center">';
        html += '<span class="text-primary icon icon-question-circle icon-5x"></span>';
        html += '<h3 class="softwareNet-modal-quetions">' + opts.message + '</h3>';
        html += '<div class="m-t-lg">';
        html += '<button id="softwareNet-modal-confirm-acept" class="btn btn-primary" data-dismiss="modal" type="button">Aceptar</button>';
        html += '<button id="softwareNet-modal-confirm-cancel" class="btn btn-default m-l-lg" data-dismiss="modal" type="button">Cancel</button>';
        html += '</div>';
        html += '</div>';
        html += '</div>';
        html += '</div>';
        html += '</div>';
        html += '</div>';

        me.Controls.ModalContentConfirm().html(html);
        me.Controls.ModalConfirm().modal();
        me.Controls.ModalConfirm().modal('show');
        return me;
    },
    ConfirmatioClose: function (opts) {
        var me = SoftwareNet.DJ.Web.Components.Message.prototype;
        me.Controls.ConfirmClose().on("click", function () {
            me.Controls.ModalConfirm().modal('hide');
            opts.callback(true);
        });
    },
    ConfirmationAcept: function (opts) {
        var me = SoftwareNet.DJ.Web.Components.Message.prototype;
        me.Controls.ConfirmAcept().on("click", function () {
            opts.callback(true);
        });
    },

    WarningCustom: function (opts) {
        opts.dialogClass = 'message-dialog-warning';
        opts.title = opts.title ? opts.title : SoftwareNet.DJ.Web.Shared.General.Resources.EtiquetaAdvertencia;
        opts.message = '<div class="alert alert-warning">' + opts.message + '</div>';

        this._privateFunction.show.apply(this, [opts]);
    },
    WarningCloseCustom: function (opts) {
        $(".ui-dialog-titlebar-close").on("click", function () {
            opts.callback(true);
        });
    },

    Warning: function (opts) {
        var me = SoftwareNet.DJ.Web.Components.Message.prototype;
        html = '<div id="modalWarning" tabindex="-1" role="dialog" class="modal fade in" data-backdrop="static" data-keyboard="false" style="z-index: 10000000 !important;display: block; padding-right: 17px;">';
        //html += '<div class="modal-dialog modal-sm">';
        html += '<div class="modal-dialog">';
        html += '<div class="modal-content">';
        html += '<div class="modal-header">';
        html += '</div>';
        html += '<div class="modal-body">';
        html += '<div class="text-center">';
        html += '<span class="text-primary icon icon-info-circle icon-5x"></span>';
        html += '<h3 class="text-primary">Aviso</h3>';
        html += '<div class="alert alert-warning">' + opts.message + '</div>';
        html += '<div class="m-t-lg">';
        html += '<button class="btn btn-default" id="softwareNet-modal-warning-close" type="button">Cerrar</button>';
        html += '</div>';
        html += '</div>';
        html += '</div>';
        html += '<div class="modal-footer"></div>';
        html += '</div>';
        html += '</div>';
        html += '</div>';

        me.Controls.ModalContentWarning().html(html);
        me.Controls.ModalWarning().modal();
        me.Controls.ModalWarning().modal('show');

        eventInt = {
            WarningClose: function (opts) {
                var me = SoftwareNet.DJ.Web.Components.Message.prototype;
                me.Controls.WarningClose().on("click", function () {
                    me.Controls.ModalWarning().modal('hide');
                    if (opts)
                        opts.callback(true);

                });               
            },

        }        
        return eventInt;
    },
    WarningClose: function (opts) {
        var me = SoftwareNet.DJ.Web.Components.Message.prototype;
        me.Controls.WarningClose().on("click", function () {
            me.Controls.ModalWarning().modal('hide');           
            if (opts)
                opts.callback(true);

        });
    },
    RestartSection: function () {
        $("body").addClass("layout layout-header-fixed modal-open");
    },

    Error: function (opts) {
        opts.dialogClass = 'message-dialog-error';
        opts.title = opts.title ? opts.title : 'Error';
        opts.message = '<div class="alert alert-danger">' + opts.message + '</div>';
        this._privateFunction.show.apply(this, [opts]);
    },

    setMessage: function (message) {
        this.divDialog.setContent(message);
    },
    appendElement: function (element1, element2) {
        this.divDialog.setElement(element1, element2);
    },
    onClose: null,

    destroy: function () {
        if (this.divDialog) {
            this.divDialog.destroy();
        }
    },

    _privateFunction: {
        createMessage: function (opts) {
            var me = this;
            this.divDialog = new SoftwareNet.DJ.Web.Components.Dialog({
                closeOnEscape: false,
                close: function (event, ui) { if (me.onClose && me.onClose != null) { me.onClose(event, ui); } },
                resizable: false,
                closeText: "Close",
                width: 'auto',
                maxWidth: '600',
                height: 'auto',
                maxHeight: '600',
                fluid: true
            });
            SoftwareNet.DJ.Web.Components.Message.prototype.Controls.divDialog = this.divDialog;

            this.divDialog.setClass('message-dialog-confirmation');
        },

        show: function (opts) {
            if (this.divDialog) {
                if (this.informationTimeOut) {
                    clearTimeout(this.informationTimeOut);
                }
                //opts.position = opts.position ? opts.position : { my: "center", at: "center", of: window };
                opts.modal = opts.modal == false ? opts.modal : true;
                //this.divDialog.setPosition(opts.position);

                this.divDialog.close();
                if (opts.modal) {
                    $(this.idDivDialogTitleBar).show();
                    if (opts.spanIcon && opts.h3Title) {
                        this.appendElement(opts.spanIcon, opts.h3Title);
                    } else {
                        this.setMessage(opts.message);
                    }

                }
                else {
                    $(this.idDivDialogTitleBar).hide();
                    this.setMessage(opts.message);
                }

                this.onClose = opts.onClose ? opts.onClose : null;
                this.divDialog.setTitle(opts.title);

                this.divDialog.setButtons(opts.buttons);
                this.divDialog.setClass(opts.dialogClass);
                this.divDialog.setModal(opts.modal);
                this.divDialog.setMinWidth(opts.minWidth);
                this.divDialog.setMinHeight(opts.minHeight);
                this.divDialog.setFluid(opts.fluid);
                this.divDialog.open();
            }
        }
    }
};