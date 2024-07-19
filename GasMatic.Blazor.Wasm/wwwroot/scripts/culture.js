window.clientCulture = {
    get: () => localStorage["client-culture"],
    set: (value) => localStorage["client-culture"] = value
};
window.changeHtmlLang = (lang) => {
    document.documentElement.lang = lang;
};