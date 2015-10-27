(function () {
    'use strict';

    angular.module('SoLaSolutions')
        .controller('MainController', MainController);

    MainController.$inject = ['$mdSidenav', '$scope'];
    function MainController($mdSidenav, $scope) {

        (function initController() {
           
        })();

        var self = this;
        
        self.toggleLeft = function() {
            $mdSidenav('left').toggle();
        }
    }

})();