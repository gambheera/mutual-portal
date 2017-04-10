
(function () {
    'use strict';

    function homeCtrl(userService) {
        var vm = this;
        vm.title = 'Homepage';

        vm.getUserInfo = function () {
            userService.getUserInfo().then(function (successRespond) {
                console.log(successRespond);
            }, function (errorRespond) {
                console.log(errorRespond);
            });
        };
    }

    homeCtrl.$inject = ['userService'];

    angular.module('mutualApp').controller('homeCtrl', homeCtrl);

})();