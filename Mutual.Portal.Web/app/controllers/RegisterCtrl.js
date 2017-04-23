(function () {
    'use strict';

    function registerCtrl($scope, localStorageService, userService) {
        var vm = this;
        vm.title = 'Register';

        vm.employeeTypeList = [{ value: 1, text: 'Nurse' }/*, { value: 2, text: 'Teacher' }, { value: 3, text: 'Govenment' }*/];

        vm.selectedEmployeeType = 0;
        vm.name = "";
        vm.mobileNumber = "";
        vm.telephoneNumber = "";
        vm.userGuid = "";


        vm.registerWithSocial = function (provider) {
            var employerType = vm.selectedEmployeeType;

            if (employerType === 0) {
                // toast message as error
                return;
            }

            // dev
            var externalProviderUrl = "/api/Account/ExternalRegister?provider=" + provider + "&employerType=" + employerType + "&response_type=token&client_id=self&redirect_uri=http%3A%2F%2Flocalhost%3A52216%2Flogin";

            //prod
            //var externalProviderUrl = "/api/Account/ExternalLogin?provider=" + provider + "&response_type=token&client_id=self&redirect_uri=http%3A%2F%2Flocalhost%3A52216%2Flogin";

            window.location = externalProviderUrl;

            //userService.register(externalProviderUrl).then(function (data) {
            //    debugger;
            //    var d = data;
            //});
            
            //Time to set token to the local storage and redirect to somewhere...
        };

        vm.selectEmployeeType = function(employeeTypeId) {
            vm.selectedEmployeeType = employeeTypeId;

            var userInfo = localStorageService.get("user_info");

            debugger;

            if (userInfo) {
                vm.userGuid = userInfo.guidString;
                vm.name = userInfo.name;
                vm.mobileNumber = userInfo.contact1;
                vm.telephoneNumber = userInfo.contact2;
            }
        };

        vm.register = function () {
            debugger;
            var obj = {
                guid: vm.userGuid,
                name: vm.name,
                contactNumber1: vm.mobileNumber,
                contactNumber2: vm.telephoneNumber,
                employmentType: vm.selectedEmployeeType
            };

            userService.register(obj).then(function(successObj) {
                // Time to forword more details asking page according to the employee type.
                if (vm.selectedEmployeeType === 1) {
                    window.location = '/NursingEmployeeDetail';
                }
            }, function(failObj) {
                debugger;
            });
        };

        var checkThisPageAccessEligibility = function() {
            var userInfo = localStorageService.get("user_info");

            if (userInfo) {
                if (userInfo.isEmployeeDetailesProvided) {
                    window.location = '/userdashboard';
                }
            }
        };

        var init = function () {
            $scope.$parent.username = userService.authenticateCurrentPosition();
            checkThisPageAccessEligibility();
        };

        init();
    }

    registerCtrl.$inject = ['$scope', 'localStorageService', 'userService'];

    angular.module('mutualApp').controller('registerCtrl', registerCtrl);

})();