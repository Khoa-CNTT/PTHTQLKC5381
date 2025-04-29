
// Lấy modal
var modal = document.getElementById("detailModal");

// Lấy nút đóng modal
var span = document.getElementsByClassName("close")[0];

// Khi click vào nút đóng, đóng modal
span.onclick = function () {
    modal.style.display = "none";
}

// Khi click bất kỳ đâu ngoài modal, đóng modal
window.onclick = function (event) {
    if (event.target == modal) {
        modal.style.display = "none";
    }
}

// Hàm hiển thị modal (có thể gọi từ code-behind)
function showModal() {
    modal.style.display = "block";
}