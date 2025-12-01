app.factory("vacancyFactory", function ($http) {
    var baseUrl = "/api/API/";

    return {
        getVacancyList: () => $http.get(baseUrl + "getVacancyList"),
        getActiveVacancyList: () => $http.get(baseUrl + "getActiveVacancyList"),
        getVacancyInfoById: id => $http.get(baseUrl + "getVacancyInfoById/" + id),
        saveVacancy: objVacancy => $http.post(baseUrl + "saveVacancy", objVacancy),
        updateActive: (id, isActive) =>
            $http.post(baseUrl + "updateVacancyActive", { id: id, isActive: isActive }),
        deleteVacancyById: id => $http.delete(baseUrl + "deleteVacancy/" + id),
    };
});