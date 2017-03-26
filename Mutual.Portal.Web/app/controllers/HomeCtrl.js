
(function () {
    'use strict';
    angular.module('mutualApp').controller('HomeCtrl', HomeCtrl);

    HomeCtrl.$inject = [];

    function HomeCtrl() {
        var vm = this;
        vm.title = 'Homepage';
    }
})();