//const update_form = document.querySelector('#patientUpdate_container');
//const add_form = document.querySelector('#patientAdd_container');
function openForm(formName) {
    const form = document.querySelector(formName);
    form.classList.add("d_block");
}

function closeForm(formName) {
    const form = document.querySelector(formName);
        form.classList.remove("d_block");
    return false;
}

//                                  Library
function showAlert(titleValue, textValue, iconValue) {
    Swal.fire({
        title: titleValue,
        text: textValue,
        icon: iconValue
    });
}

function showAlert(titleValue, iconValue) {
    Swal.fire({
        title: titleValue,
        icon: iconValue
    });
}















