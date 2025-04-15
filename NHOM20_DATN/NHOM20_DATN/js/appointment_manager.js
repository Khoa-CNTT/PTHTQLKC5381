
function openForm(formName) {
    const form = document.querySelector(formName);
    form.classList.add("d_block");
}

function closeForm(formName) {
    const form = document.querySelector(formName);
    form.classList.remove("d_block");
}

