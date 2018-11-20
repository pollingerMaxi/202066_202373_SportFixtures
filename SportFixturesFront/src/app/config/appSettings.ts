export class AppSettings {
    public static readonly BaseUrl: string = "http://localhost:50144/api/";
    public static readonly localstorageToken: string = "userToken";
    public static readonly localstorageUser: string = "user";

    public static readonly ApiEndpoints: any = {
        login: `${AppSettings.BaseUrl}login`,
        logout: `${AppSettings.BaseUrl}logout`,
        getAllUsers: `${AppSettings.BaseUrl}users`,
        addSport: `${AppSettings.BaseUrl}sports`,
        getSports: `${AppSettings.BaseUrl}sports`,
        updateSport: `${AppSettings.BaseUrl}sports`,
        deleteSport: `${AppSettings.BaseUrl}sports/`,
        getTeams: `${AppSettings.BaseUrl}teams`,
        getSportById: `${AppSettings.BaseUrl}sports/`,
        addTeam: `${AppSettings.BaseUrl}teams`,
        updateTeam: `${AppSettings.BaseUrl}teams`,
        deleteTeam: `${AppSettings.BaseUrl}teams/`,
        getFavorites: `${AppSettings.BaseUrl}users/favorites/`,
        getEncountersOfTeam: `${AppSettings.BaseUrl}encounters/team/`,
        getCommentsOfEncounter: `${AppSettings.BaseUrl}comments/encounter/`,
        addUser: `${AppSettings.BaseUrl}users`,
        updateUser: `${AppSettings.BaseUrl}users`,
        deleteUser: `${AppSettings.BaseUrl}users/`,
        followTeam: `${AppSettings.BaseUrl}users/favorite`,
        getLogs: `${AppSettings.BaseUrl}logger?from={0}&to={1}`,
        getPositions: `${AppSettings.BaseUrl}positions/`,
        addEncounter: `${AppSettings.BaseUrl}encounters`,
        updateEncounter: `${AppSettings.BaseUrl}encounters`,
        deleteEncounter: `${AppSettings.BaseUrl}encounters/`,
        getEncounters: `${AppSettings.BaseUrl}encounters`,
    };

    public static readonly RouterUrls: any = {
        home: "home",
        login: "login",
        sportsManagement: "sportsManagement",
        teamsManagement: "teamsManagement",
        usersManagement: "usersManagement",
        actionsReport: "actionsReport",
        positionsTable: "positionsTable",
        encountersManagement: "encountersManagement"
    };
}