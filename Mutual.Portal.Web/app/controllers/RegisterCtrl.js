(function () {
    'use strict';
    angular.module('mutualApp').controller('RegisterCtrl', RegisterCtrl);

    RegisterCtrl.$inject = [];

    function RegisterCtrl() {
        var vm = this;
        vm.title = 'Homepage';
    }
})();