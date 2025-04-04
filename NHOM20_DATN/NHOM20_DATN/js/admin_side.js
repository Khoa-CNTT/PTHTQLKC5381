let startX = 0;
let sidebar = document.getElementById('sidebar');
let isDragging = false;
let menu = document.getElementById('btn_menu');

// Touch event handling for mobile devices
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





