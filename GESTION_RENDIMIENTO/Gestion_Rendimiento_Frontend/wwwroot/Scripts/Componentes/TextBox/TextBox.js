ns('SoftwareNet.DJ.Web.Components');
SoftwareNet.DJ.Web.Components.TextBox = {
    Options: {
        NameMask: 'Mask'
    },

    Function: {
        Configure: function (idContainer) {

            var inputs = new Array();

            if (idContainer == undefined) {
                inputs = $('input[type=text], input[type=password], textarea');
            } else {
                inputs = $('#' + idContainer).find('input[type=text], input[type=password], textarea')
            }

            $.each(inputs, function (index, value) {
                var input = $(value);

                input.bind('drop', SoftwareNet.DJ.Web.Components.TextBox.Event.General.drop);

                var type = input.attr(SoftwareNet.DJ.Web.Components.TextBox.Options.NameMask);

                if (typeof type !== typeof undefined && type !== false) {
                    switch (type) {
                        case 'decimal':
                            SoftwareNet.DJ.Web.Components.TextBox.Function.ApplyDecimal(input);
                            break;
                        case 'integer':
                            SoftwareNet.DJ.Web.Components.TextBox.Function.ApplyInteger(input);
                            break;
                        case 'date':
                            SoftwareNet.DJ.Web.Components.TextBox.Function.ApplyDate(input);
                            break;
                    }
                }
            });

            inputs = undefined;
        },

        // N : Numérico
        // AN: AlfaNumérico
        ApplyTypeDocument: function (input, configuration) {
            //configuration = {
            //    maxLength: 8,
            //    dataType: 'N'
            //}
            var maxLength = 20;
            var dataType = 'AN';

            if ((typeof configuration.maxLength !== typeof undefined)) {
                maxLength = configuration.maxLength;
            }

            if ((typeof configuration.dataType !== typeof undefined)) {
                dataType = configuration.dataType;
            }

            input.val('');

            input.bind('drop', SoftwareNet.DJ.Web.Components.TextBox.Event.General.drop);
            input.attr('maxlength', maxLength);
            input.attr('lengthNumber', maxLength);
            input.unbind('paste');
            input.unbind('keypress');

            switch (dataType) {
                case 'N':
                    input.bind('paste', SoftwareNet.DJ.Web.Components.TextBox.Event.Numerico.paste);
                    input.bind('keypress', SoftwareNet.DJ.Web.Components.TextBox.Event.Numerico.keypress);
                    break;
                case 'AN':
                    input.bind('paste', SoftwareNet.DJ.Web.Components.TextBox.Event.AlfaNumerico.paste);
                    input.bind('keypress', SoftwareNet.DJ.Web.Components.TextBox.Event.AlfaNumerico.keypress);
                    break;
                default:
                    input.bind('paste', SoftwareNet.DJ.Web.Components.TextBox.Event.AlfaNumerico.paste);
                    input.bind('keypress', SoftwareNet.DJ.Web.Components.TextBox.Event.AlfaNumerico.keypress);
                    break;
            }
        },

        ApplyDecimal: function (input) {
            var maxlength = input.attr('maxlength');

            if (!(typeof maxlength !== typeof undefined && maxlength !== false)) {
                maxlength = 4;
            }

            maxlength = parseInt(maxlength);

            var largeDecimal = 3; //tamaño decimal
            var countSeparatorMiles = parseInt(maxlength / 3); //numero de separadores

            var newMaxlength = maxlength + largeDecimal + countSeparatorMiles

            input.attr('maxlength', newMaxlength);
            input.attr('lengthNumber', maxlength);

            input.blur(SoftwareNet.DJ.Web.Components.TextBox.Event.Decimal.blur);
            input.bind('paste', SoftwareNet.DJ.Web.Components.TextBox.Event.Decimal.paste);
            input.keypress(SoftwareNet.DJ.Web.Components.TextBox.Event.Decimal.keypress);
        },

        ApplyInteger: function (input) {
            var maxlength = input.attr('maxlength');

            if (!(typeof maxlength !== typeof undefined && maxlength !== false)) {
                maxlength = 4;
            }

            maxlength = parseInt(maxlength);

            var largeDecimal = 0; //tamaño decimal
            var countSeparatorMiles = parseInt(maxlength / 3); //numero de separadores

            var newMaxlength = maxlength + largeDecimal + countSeparatorMiles

            input.attr('maxlength', newMaxlength);
            input.attr('lengthNumber', maxlength);

            input.blur(SoftwareNet.DJ.Web.Components.TextBox.Event.Integer.blur);
            input.bind('paste', SoftwareNet.DJ.Web.Components.TextBox.Event.Integer.paste);
            input.keypress(SoftwareNet.DJ.Components.TextBox.Event.Integer.keypress);

        },

        ApplyDatetime: function (input) {
        },

        FormatDecimal: function (options) {
            $.formatCurrency.regions[''].symbol = (options.symbol == undefined) ? '' : options.symbol;
            $.formatCurrency.regions[''].decimalSymbol = (options.decimalSymbol == undefined) ? SoftwareNet.DJ.Web.Global.Format.DecimalSeparatorDecimal : options.decimalSymbol;
            $.formatCurrency.regions[''].digitGroupSymbol = (options.digitGroupSymbol == undefined) ? SoftwareNet.DJ.Web.Global.Format.DecimalSeparatorMiles : options.digitGroupSymbol;

            options.input.formatCurrency({
                roundToDecimalPlace: 2,
                eventOnDecimalsEntered: true
            });
        },

        FormatInteger: function (options) {
            $.formatCurrency.regions[''].symbol = (options.symbol == undefined) ? '' : options.symbol;
            $.formatCurrency.regions[''].decimalSymbol = (options.decimalSymbol == undefined) ? '-' : options.decimalSymbol;
            $.formatCurrency.regions[''].digitGroupSymbol = (options.digitGroupSymbol == undefined) ? SoftwareNet.DJ.Web.Global.Format.IntegerSeparatorMiles : options.digitGroupSymbol;

            options.input.formatCurrency({
                roundToDecimalPlace: 0,
                eventOnDecimalsEntered: true
            });
        }
    },

    Event: {
        General: {
            drop: function () {
                return false;
            }
        },

        Decimal: {
            blur: function (input) {
                SoftwareNet.DJ.Web.Components.TextBox.Function.FormatDecimal({
                    input: $(this)
                });
            },

            paste: function (e) {
                var cadena = obtenerValorCopy(e);
                var patron = '^[0-9]{1,9}(\\' + SoftwareNet.DJ.Web.Global.Format.DecimalSeparatorDecimal + '[0-9]{0,2})?$';

                var regexp = new RegExp(patron);

                if (!cadena.search(patron))
                    return true;
                else
                    return false;
            },

            keypress: function (evento) {
                var keyCode = obtenerKeyCode(evento);
                var input = $(this);

                // backspace
                if (keyCode == 8) { return true; }

                // direccionales
                if (evento.charCode == 0) { return true; }


                // 0-9
                if (keyCode > 47 && keyCode < 58) {

                    var selectedText = null;

                    if (window.getSelection) // Firefox
                    {
                        selectedText = input.val().substring(document.activeElement.selectionStart, document.activeElement.selectionEnd);
                    }
                    else // IE
                    {
                        selectedText = document.selection.createRange().text;
                    }

                    if (selectedText != null && selectedText.length == input.val().length) {
                        input.val('');
                    }

                    var lengthNumber = input.attr('lengthNumber');

                    if (input.val() != undefined && input.val().indexOf(SoftwareNet.DJ.Web.Global.Format.DecimalSeparatorDecimal) == -1 && input.val().length == lengthNumber) {
                        return false;
                    }

                    if (input.val() == '') { return true; }
                    var patron = '.{7}[0-9]{2}$';
                    var regexp = new RegExp(patron);
                    return !(regexp.test(input.val()));
                }

                // Separador Decimal
                if (keyCode == SoftwareNet.DJ.Web.Global.Format.DecimalSeparatorDecimal.charCodeAt(0)) {
                    if (input.val() == '') { return false; }
                    regexp = /^[0-9]+$/;
                    return regexp.test(input.val());
                }

                // other key
                return false;
            }
        },

        Integer: {
            blur: function () {
                SoftwareNet.DJ.Web.Components.TextBox.Function.FormatInteger({
                    input: $(this)
                });
            },

            paste: function (e) {
                return validarCopySoloNumerico(e);
            },

            keypress: function (evento) {
                var keyCode = obtenerKeyCode(evento);
                var input = $(this);

                // backspace
                if (keyCode == 8) { return true; }

                // direccionales
                if (evento.charCode == 0) { return true; }

                // 0-9
                if (keyCode > 47 && keyCode < 58) {

                    var selectedText = null;

                    if (window.getSelection) // Firefox
                    {
                        selectedText = input.val().substring(document.activeElement.selectionStart, document.activeElement.selectionEnd);
                    }
                    else // IE
                    {
                        selectedText = document.selection.createRange().text;
                    }

                    if (selectedText != null && selectedText.length == input.val().length) {
                        input.val('');
                    }

                    var lengthNumber = input.attr('lengthNumber');

                    if (input.val() != undefined && input.val().length == lengthNumber) {
                        return false;
                    }

                    if (input.val() == '') { return true; }
                    var patron = '[0-9]';
                    var regexp = new RegExp(patron);
                    return (regexp.test(input.val()));
                }

                // other key
                return false;
            }
        },

        Numerico: {
            paste: function (e) {
                return validarCopySoloNumerico(e);
            },

            keypress: function (evento) {

                var input = $(this);

                var selectedText = null;

                if (window.getSelection) // Firefox
                {
                    selectedText = input.val().substring(document.activeElement.selectionStart, document.activeElement.selectionEnd);
                }
                else // IE
                {
                    selectedText = document.selection.createRange().text;
                }

                if (selectedText != null && selectedText.length == input.val().length) {
                    input.val('');
                }

                var lengthNumber = input.attr('lengthNumber');

                if (input.val() != undefined && input.val().length == lengthNumber) {
                    return false;
                }

                return validarSoloNumeros(evento);
            }
        },

        AlfaNumerico: {
            paste: function (e) {
                return ValidarCopySoloAlfanumericoGuionEspacioBlanco(e);
            },

            keypress: function (evento) {
                return ValidarCopySoloAlfanumericoGuionEspacioBlanco(evento);
            }
        }
    }
};