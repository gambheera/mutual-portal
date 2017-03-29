
(function () {
    'use strict';
    angular.module('mutualApp').controller('UserDashBoardCtrl', UserDashBoardCtrl);

    UserDashBoardCtrl.$inject = ['Userservice'];

    function UserDashBoardCtrl(Userservice) {
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