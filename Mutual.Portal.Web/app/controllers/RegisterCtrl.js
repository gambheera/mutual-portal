(function () {
    'use strict';

    function registerCtrl() {
        var vm = this;
        vm.title = 'Register';
         
        var _init = function () {

        };

        vm.registerWithSocial = function (provider, employerType) {
            // dev
            var externalProviderUrl = "/api/Account/ExternalRegister?provider=" + provider + "&employerType=" + employerType + "&response_type=token&client_id=self&redirect_uri=http%3A%2F%2Flocalhost%3A52216%2Flogin";

            //prod
            //var externalProviderUrl = "/api/Account/ExternalLogin?provider=" + provider + "&response_type=token&client_id=self&redirect_uri=http%3A%2F%2Flocalhost%3A52216%2Flogin";

            window.location = externalProviderUrl;

            //Time to set token to the local storage and redirect to somewhere...
        };

        _init();
    }

    registerCtrl.$inject = [];

    angular.module('mutualApp').controller('registerCtrl', registerCtrl);

})();