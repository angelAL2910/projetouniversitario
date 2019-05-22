﻿function pepViewModel() {
    var self = this;

    //This method helps serialization to get the exact info
    self.toJSON = function () {
        var copy = ko.toJS(self);
        //delete copy.toString;
        delete copy.validationRules;
        return copy;
    };

    self.id = ko.observable(Math.floor((Math.random() * 10000) + 1) * -1);
    self.driverID = ko.observable();
    self.fullNamePep = ko.observable();
    self.RelationshipIdPep = ko.observable(0);
    self.PositionPep = ko.observable();
    self.fromYear = ko.observable(0).extend({ numeric: 0 });
    self.toYear = ko.observable(0).extend({ numeric: 0 });

    self.clear = function () {
        self.driverID(0);
        self.fullNamePep('');
        self.RelationshipIdPep(0);
        self.PositionPep('');
        self.fromYear(0).extend({ numeric: 0 });
        self.toYear(0).extend({ numeric: 0 });
    }


    /*Si deciden que estos campos seran requeridos, solo descomentar*/
    self.validationRules = {
        rules: {
            fullNamePep: {
                nameSurname: true,
                required: true,
                maxlength: 300
            },

            RelationshipIdPep: {
                required: true,
            },

            PositionPep: {
                nameSurname: true,
                required: true,
                maxlength: 1000
            },

            fromYear: {
                //required: true,
                number: true,
                biggerThan: "#toYear"
            },

            toYear: {
                //required: true,
                number: true,
                lessThan: "#fromYear"
            }
        },
        messages: {

            fullNamePep: {
                nameSurname: 'El Nombre Completo sólo permite caracteres alfabéticos.',
                required: 'El Nombre Completo es requerido.',
                maxlength: 'El Nombre Completo no puede tener más de 300 caracteres de longitud.'
            },

            RelationshipIdPep: {
                required: 'El Parentezco es requerido.'
            },

            PositionPep: {
                nameSurname: 'La Posición y/o Cargo sólo permite caracteres alfabéticos.',
                required: 'La Posición y/o Cargo es requerido.',
                maxlength: 'La Posición y/o Cargo no puede tener más de 1000 caracteres de longitud.'
            },

            fromYear: {
                //required: 'El campo Desde es requerido.',
                number: 'El campo Desde debe ser un numero.',
                //biggerThan: 'El A&ntilde;o Desde no puede ser mayor que el A&ntilde;o Hasta.'
            },

            toYear: {
                //required: 'El campo Hasta es requerido.',
                number: 'El campo Hasta debe ser un numero.',
                //lessThan: 'El A&ntilde;o Hasta no puede ser menor que el A&ntilde;o Desde.'
            }
        }
    };
    //

    self.loadModel = function (model) {
        self.id(model.Id);
        self.driverID(model.Persons_Pep);
        self.fullNamePep(model.Name);
        self.RelationshipIdPep(model.RelationshipId);
        self.PositionPep(model.Position);
        self.fromYear(model.FromYear);
        self.toYear(model.ToYear);
    }

    self.pepDataComplete = function () {
        
        var completed = self.fullNamePep()
            //&& self.RelationshipIdPep()
            && self.PositionPep()
            //&& self.fromYear()
            //&& self.toYear()
            /*&& (self.fromYear() || self.fromYear()>0)
            && (self.toYear() || self.toYear() > 0)*/
            != undefined

        return completed;
    };
}