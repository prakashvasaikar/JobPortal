app.factory("vacancyFactory", function ($http) {
    var baseUrl = "/api/API/";

    return {
        getVacancyList: () => $http.get(baseUrl + "getVacancyList"),
        getVacancyInfoById: id => $http.get(baseUrl + "getVacancyInfoById" + id),
        saveVacancy: objVacancy => $http.post(baseUrl + "saveVacancy", objCandidateDetail),
    };
});