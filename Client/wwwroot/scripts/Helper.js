export function showAlert(message) {
    alert(message);
};

export function SetFocusToElement (element)  {
    element.focus();
};

export function SetBusinessDropDownIndex(index) {
    document.getElementById("SelectBusiness").selectedIndex = index;
};

export function SetLanguageDropDownIndex(index) {
    document.getElementById("SelectLanguage").selectedIndex = index;
};
