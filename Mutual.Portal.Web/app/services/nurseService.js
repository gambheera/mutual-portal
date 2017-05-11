(function () {
    'use strict';

    function nurseService($http, $q, localStorageService, httpService) {

        function getHospitalList() {
            return httpService.get('/api/nurse/get-hospital-list').then(function (response) {
                return response;
            });
        };

        function getIndividualUserDetails(requesteeGuid) {
            return httpService.get('/api/nurse/get-individual-nurse?requesteeGuid=' + requesteeGuid).then(function(respond) {
                return respond;
            });
        };

        function saveNurse(nurseObj) {
            return httpService.post('/api/nurse/save-nurse', nurseObj).then(function (response) {
                return response;
            });
        };

        function searchNurses(currentHospitalId, dreamHospitalId, pageNumber) {
            var url = '/api/nurse/search-nurses?currentHospitalId=' + currentHospitalId + '&dreamHospitalId=' + dreamHospitalId + '&pageNumber=' + pageNumber;
            return httpService.get(url).then(function (response) {
                return response;
            });
        };

        function getIndividualProfileDetails(requesteeGuid) {
            var url = '/api/nurse/get-individual-profile-details?requesteeGuid=' + requesteeGuid;
            return httpService.get(url).then(function (response) {
                return response;
            });
        };

        function getContactDetails(requesteeGuid) {
            var url = '/api/nurse/get-contact-details?requesteeGuid=' + requesteeGuid;
            return httpService.get(url).then(function (response) {
                return response;
            });
        }

        var service = {
            getIndividualUserDetails: getIndividualUserDetails,
            getHospitalList: getHospitalList,
            saveNurse: saveNurse,
            searchNurses: searchNurses,
            getIndividualProfileDetails: getIndividualProfileDetails,
            getContactDetails: getContactDetails
        };

        return service;
    }

    nurseService.$inject = ['$http', '$q', 'localStorageService', 'httpService'];

    angular.module('mutualApp').factory('nurseService', nurseService);
})();