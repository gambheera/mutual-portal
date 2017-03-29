(function () {
    'use strict';
    angular.module('mutualApp').controller('LoginCtrl', LoginCtrl);

    LoginCtrl.$inject = ['$scope', 'localStorageService', 'MvcNavigationService'];

    function LoginCtrl($scope, localStorageService, MvcNavigationService) {
        var vm = this;
        vm.title = 'Homepage';

        $scope.$parent.title = "Controlled by chiled";

        var _init = function () {
            _checkLocationHash();
        };

        vm.loginWithSocial = function (provider) {
            // dev
            var externalProviderUrl = "/api/Account/ExternalLogin?provider=" + provider + "&response_type=token&client_id=self&redirect_uri=http%3A%2F%2Flocalhost%3A52216%2Flogin";

            //prod
            //var externalProviderUrl = "/api/Account/ExternalLogin?provider=" + provider + "&response_type=token&client_id=self&redirect_uri=http%3A%2F%2Flocalhost%3A52216%2Flogin";

            window.location = externalProviderUrl;

            //Time to set token to the local storage and redirect to somewhere...
            
        };

        var _checkLocationHash = function(){
            if(location.hash){
                if(location.hash.split('access_token=')){
                    var accessToken = location.hash.split('access_token=')[1].split('&')[0];
                    if(accessToken){
                        localStorageService.set("user_access_token", accessToken);

                        // Time to go to the dashboard page
                        MvcNavigationService.navigate('/userdashboard');
                    }
                }
            }
        };

        _init();
    }
})();