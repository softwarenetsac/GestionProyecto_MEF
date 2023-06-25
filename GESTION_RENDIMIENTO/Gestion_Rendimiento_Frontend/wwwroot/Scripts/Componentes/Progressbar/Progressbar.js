ns('SoftwareNet.DJ.Web.Components');
SoftwareNet.DJ.Web.Components.ProgressBar = function (opts) {
    this.init(opts);
};

SoftwareNet.DJ.Web.Components.ProgressBar.prototype = {
    classDialogTitlebar: '.ui-dialog-titlebar',
    idDivProgrees: 'divProgressSolugen',
    idDivDialog: 'divProgressDialogSolugen',
    isExecuteProgress: false,
    uiHeader: function () { return $('.ui-dialog-titlebar'); },
    init: function (opts) {
        this._privateFunction.createProgressBar.apply(this, [opts]);
    },

    setMaxValue: function (max) {
        if (this.divProgress) {
            this.divProgress.progressbar('option', 'max', max);
        }
    },

    setValue: function (value) {
        if (this.isExecuteProgress && this.divProgress) {
            this.divProgress.progressbar('option', 'value', value);
        }
    },

    getValue: function () {
        var value = null;
        if (this.divProgress) {
            value = this.divProgress.progressbar('option', 'value');
        }
        return value;
    },

    show: function () {
        if (this.divDialog) {
            this.divDialog.dialog('open');
        }
        else {
            if (this.divProgress) {
                this.divProgress.show();
            }
        }
    },

    hide: function () {
        if (this.divDialog) {
            this.divDialog.dialog('close');
        }
        else {
            if (this.divProgress) {
                this.divProgress.hide();
            }
        }
    },

    destroy: function () {
        if (this.divProgress) {
            this.divProgress.progressbar('destroy');
            this.divProgress.remove();
        }

        if (this.divDialog) {
            this.divDialog.dialog('destroy');
            this.divDialog.remove();
        }
    },

    _privateFunction: {
        createProgressBar: function (opts) {
            if (!opts.targetLoading) {
                this.divDialog = this._privateFunction.implementDialogElement.apply(this, [opts]);
                opts.targetLoading = this.divDialog;
            }
            else {
                opts.targetLoading = $('#' + opts.targetLoading);
            }

            this.divProgress = this._privateFunction.implementProgressElement.apply(this, [opts.targetLoading, opts.maxValue]);
            //ocultamos el header de dialogo
            //this.uiHeader().attr("style", "display:none");
        },

        implementProgressElement: function (targetLoading, maxValue) {
            var divProgress = $('#' + this.idDivProgrees);
            if (divProgress.length == 0) {
                divProgress = $('<div />');
                divProgress.attr('id', this.idDivProgrees);
                divProgress.addClass("progressBar-SoftwareNet");
                divProgress.append($('<div class="progressBar-SoftwareNet-label"><div class="spinner spinner-primary spinner-lg pos-r sq-100"></div></div>'));
                $(divProgress).remove('.ui-progressbar-value');
                //$('.ui-dialog-titlebar').attr("style", "display:none");
                $(this.classDialogTitlebar).hide();

                //if (SoftwareNet.DJ.Web.Shared.General.Resources) {
                //    divProgress.find('.progressBar-SoftwareNet').text(SoftwareNet.DJ.Web.Shared.General.Resources.EtiquetaCargando);
                //}

                targetLoading.append(divProgress);
            }

            if (targetLoading) {
                divProgress.css('position', 'relative');
                divProgress.css('top', '0px');
                divProgress.css('left', '0px');
            }

            var me = this;
            var config = {
                value: maxValue ? 0 : false,
                change: function () {
                    if (me.getValue() != false) {
                        //divProgress.find('.progressBar-SoftwareNet-label').text(me.getValue() + "%");
                    }
                }
            };

            if (maxValue) {
                config.max = maxValue;
            }

            //para que  o se va usar un progreso            
            if (this.isExecuteProgress) {
                divProgress.progressbar(config);
                isExecuteProgress = false;
            }

            return divProgress;
        },

        implementDialogElement: function (opts) {
            var div = $('#' + this.idDivDialog);

            if (div.length == 0) {
                div = $('<div />');
                div.attr('id', this.idDivDialog);
                $('body').append(div);

            }

            div.dialog({
                dialogClass: "no-close-dialog",
                closeOnEscape: false,
                height: 150,
                width: 270,
                modal: opts.modal ? opts.modal : true,
                resizable: false,
                zIndex: 100000,
                title: 'Cargando...'
            });
            $(this.classDialogTitlebar).hide();            
            return div;
        }
    }
};