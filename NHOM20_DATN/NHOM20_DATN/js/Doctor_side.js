let startX = 0;
let sidebar = document.getElementById('sidebar');
let isDragging = false;
let menu = document.getElementById('btn_menu');

// Touch event handling for mobile devices
document.addEventListener('touchstart', function (e) {
    startX = e.touches[0].clientX;
});

//document.addEventListener('touchmove', function (e) {
//    let touchX = e.touches[0].clientX;
//    let touchDiff = touchX - startX;

//    if (touchDiff > 50 && !sidebar.classList.contains('active')) {
//        sidebar.classList.add('active');
//        menu.classList.add('d_none');
//    }

//    if (touchDiff < -50 && sidebar.classList.contains('active')) {
//        sidebar.classList.remove('active');

//    }
//});
// for mouse
document.addEventListener('mousedown', function (e) {
    startX = e.clientX;
    isDragging = true;
});


document.addEventListener('mousemove', function (e) {
    if (!isDragging) return;
    let currentX = e.clientX;
    let diffX = currentX - startX;

    if (diffX > 50 && !sidebar.classList.contains('active')) {
        sidebar.classList.add('active');
        menu.classList.add('d_none');

    }

    if (diffX < -50 && sidebar.classList.contains('active')) {
        sidebar.classList.remove('active');
        menu.classList.remove('d_none');

    }
});
document.addEventListener('mouseup', function () {
    isDragging = false;
});



function close_bar() {
    //
    sidebar.classList.remove('active');
    menu.classList.remove('d_none');
}

function display_bar() {
    sidebar.classList.add('active');
    menu.classList.add('d_none');
}
document.onclick = function (e) {
    if (!sidebar.contains(e.target) && !menu.contains(e.target)) {
        sidebar.classList.remove('active');
        menu.classList.remove('d_none');

    }
}


function display() {
    const d = document.querySelector("#dropdown-menu");
    if (d.classList.contains("d_block")) {
        d.classList.remove("d_block");
        return;
    }
    d.classList.add("d_block");
}

function none() {
    const d = document.querySelector("#dropdown-menu");
    if (d.classList.contains("d_block")) {
        d.classList.remove("d_block");
    }
}


function showAlert(titleValue, textValue, iconValue ) {
    Swal.fire({
        title: titleValue,
        text: textValue,
        icon: iconValue
    });
}


