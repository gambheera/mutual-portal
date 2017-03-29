﻿
(function () {
    'use strict';
    angular.module('mutualApp').controller('HomeCtrl', HomeCtrl);

    HomeCtrl.$inject = ['Userservice'];

    function HomeCtrl(Userservice) {
        var vm = this;
        vm.title = 'Homepage';

        vm.getUserInfo = function () {
            Userservice.getUserInfo().then(function (successRespond) {
                console.log(successRespond);
            }, function (errorRespond) {
                console.log(errorRespond);
            });
        };
    }
})();