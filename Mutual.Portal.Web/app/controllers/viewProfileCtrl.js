(function () {
    'use strict';

    function viewProfileCtrl($scope, swalService, localStorageService, userService, nurseService) {
        var vm = this;
        vm.title = 'Homepage';
        vm.theRequestee = {};

        vm.getContactDetails = function() {
            nurseService.getContactDetails(vm.theRequestee.guid).then(function (data) {
                if (data.data.metaData.isSucceeded) {
                    if (data.data.data) {
                        // Data has been transfered successfully
                        vm.theRequestee.name = data.data.data.name;
                        vm.theRequestee.email = data.data.data.email;
                        vm.theRequestee.contact1 = data.data.data.contact1;
                        vm.theRequestee.contact2 = data.data.data.contact2;
                    } else {
                        swalService.info(data.data.metaData.message, "We are sorry");
                    }
                } else {
                    swalService.info(data.data.metaData.message, "We are sorry");
                }
            });
        };

        var prepareProfileData = function (obj) {
            console.log(obj);
            var hospitalList = "";
            if (obj.dreamHospitalList) {
                for (var j = 0; j < obj.dreamHospitalList.length; j++) {
                    var tempHospital = obj.dreamHospitalList[j].hospital;
                    if (j === (obj.dreamHospitalList.length - 2)) {
                        hospitalList = hospitalList + tempHospital.name + " and ";
                    } else if (j === (obj.dreamHospitalList.length - 1)) {
                        hospitalList = hospitalList + tempHospital.name;
                    } else {
                        hospitalList = hospitalList + tempHospital.name + ", ";
                    }
                }
            }

            var description = "I'm currently woring at " + obj.hospital.name + " (" + obj.hospital.districtString + " district). I'm expecting to go " + hospitalList;

            var element = {
                description: description,
                code: obj.user.code,
                lastLogin: obj.user.lastLoginOnString,
                guid: obj.user.guidString,
                contact1: obj.user.contactNumber1,
                contact2: obj.user.contactNumber2,
                email: obj.user.email,
                name: obj.user.name,
                viewCount: (obj.user.myCurrentViewCount + obj.user.myTotalViewCount)
            };

            vm.theRequestee = element;
            console.log(element);
        };

        var getProfileData = function () {
            var requesteeGuid = localStorageService.get('requestee-guid');

            if (requesteeGuid) {
                nurseService.getIndividualProfileDetails(requesteeGuid).then(function (data) {
                    if (data.data.metaData.isSucceeded) {
                        prepareProfileData(data.data.data);
                    }
                });
            }
        };

        function init() {
            $scope.$parent.username = userService.authenticateCurrentPosition();
            getProfileData();
        };

        init();
    }

    viewProfileCtrl.$inject = ['$scope', 'swalService', 'localStorageService', 'userService', 'nurseService'];

    angular.module('mutualApp').controller('viewProfileCtrl', viewProfileCtrl);

})();