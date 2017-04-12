(function () {
    'use strict';
    
    function loginCtrl($scope, $rootScope, localStorageService, mvcNavigationService, userService) {
        var vm = this;
        vm.title = 'Homepage';
        vm.token = "";
        $scope.$parent.title = "Controlled by chiled";

        vm.loginWithSocial = function (provider) {
            // dev
            var externalProviderUrl = "/api/Account/ExternalLogin?provider=" + provider + "&response_type=token&client_id=self&redirect_uri=http%3A%2F%2Flocalhost%3A52216%2Flogin";

            //prod
            //var externalProviderUrl = "/api/Account/ExternalLogin?provider=" + provider + "&response_type=token&client_id=self&redirect_uri=http%3A%2F%2Flocalhost%3A52216%2Flogin";

            window.location = externalProviderUrl;

            //Time to set token to the local storage and redirect to somewhere...
            
        };

        vm.userInfo = function () {
            userService.getUserInfo(vm.token).then(function (s) {
                console.log(s);
            }, function (f) {
                console.log(f);
            });
        };

        vm.getUserEmployeeType = function() {
            userService.getUserEmployeeType().then(function(successResponse) {
                console.log(successResponse);
                alert('Success');
            }, function(failedResponse) {
                console.log(failedResponse);
                alert('Failed');
            });
        };

        var checkLocationHash = function () {
            if(location.hash){
                if(location.hash.split('access_token=')){
                    var accessToken = location.hash.split('access_token=')[1].split('&')[0];
                    if (accessToken) {
                        localStorageService.set("user_access_token", accessToken);
                        userService.getUserInfo().then(function (s) {
                            localStorageService.set("user_info", s.data.data);
                            debugger;
                            if (!s.data.data.isRegistrationConfirmed) {
                                window.location = '/register';
                            } else if (!s.data.data.isEmployeeDetailesProvided) {
                                if (s.data.data.employmentType === 1) {
                                    // This is a nurse.
                                    window.location = '/nursingEmployeeDetail';
                                }
                            } else {
                                window.location = '/userDashboard';
                            }
                            
                        }, function(f) {
                            console.log(f);
                        });
                    }
                }
            }
        };

        var init = function() {
            checkLocationHash();
        };

        init();
    }

    loginCtrl.$inject = ['$scope', '$rootScope', 'localStorageService', 'mvcNavigationService', 'userService'];

    angular.module('mutualApp').controller('loginCtrl', loginCtrl);

})();