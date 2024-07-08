window.clientCulture = {
    get: () => localStorage["client-culture"],
    set: (value) => localStorage["client-culture"] = value
};