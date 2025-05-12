let inputIndex = 0;

function addTextInput() {
    //count index 
    const allInputs = document.querySelectorAll("textarea[name='dynamicInput'], input[name='dynamicInput']");
    inputIndex = getMaxDivDataIndex();
    var index = inputIndex++;
    const container = document.getElementById("dynamicInputs");
    const wrapper = document.createElement("div");
    const textarea = document.createElement("textarea");
    wrapper.classList.add("form-group-dynamic");

    textarea.name = "dynamicInput";
    textarea.classList.add("form-control");
    textarea.setAttribute("data-type", "text");
    textarea.setAttribute("data-index", index);
    textarea.placeholder = "Nhập nội dung...";
    textarea.rows = 3;
    textarea.cols = 40;

    const deleteBtn = document.createElement("button");
    
    deleteBtn.type = "button";
    deleteBtn.classList.add("btn-delete-dynamic");
    deleteBtn.style.marginLeft = "8px";
    deleteBtn.onclick = () => wrapper.remove();

    deleteBtn.appendChild(iconMinus());
    wrapper.appendChild(textarea);
    wrapper.appendChild(deleteBtn);
    container.appendChild(wrapper);
}



function addFileInput() {
    //count Index
    const allInputs = document.querySelectorAll("textarea[name='dynamicInput'], input[name='dynamicInput']");
    inputIndex = getMaxDivDataIndex();
    var index = inputIndex++;
    const container = document.getElementById("dynamicInputs");
    const fileInputs = container.querySelectorAll("input[type='file']");

    if (fileInputs.length >= 4) return;

    const wrapper = document.createElement("div");
    wrapper.classList.add("form-group-dynamic-img");
    const input = document.createElement("input");
    input.type = "file";
    input.name = "dynamicInput";
    
    input.setAttribute("data-type", "file");
    input.setAttribute("data-index", index);

    const fileLabel = document.createElement("span");
    fileLabel.id = "hiddenImg";
    fileLabel.style.display = "none";
    fileLabel.style.marginLeft = "8px";
    fileLabel.style.fontStyle = "italic";


    const deleteBtn = document.createElement("button");
    deleteBtn.classList.add("btn-delete-dynamic");
    deleteBtn.type = "button";
    deleteBtn.style.marginLeft = "8px";
    deleteBtn.onclick = () => {
        wrapper.remove();
        // Hiện lại nút nếu có ít hơn 4 input file
        if (container.querySelectorAll("input[type='file']").length < 4) {
            document.getElementById("addFileBtn").style.display = "inline-block";
        }
    };
    deleteBtn.appendChild(iconMinus());
    wrapper.appendChild(input);
    wrapper.appendChild(fileLabel);
    wrapper.appendChild(deleteBtn);
    container.appendChild(wrapper);

    if (container.querySelectorAll("input[type='file']").length >= 4) {
        document.getElementById("addFileBtn").style.display = "none";
    }
}

function addBV() {
    const formData = new FormData();

    let isValid = true;
    let errorMessage = '';

    // Lấy tất cả input theo name chung
    const allInputs = document.querySelectorAll("textarea[name='dynamicInput'], input[name='dynamicInput']");
    const tieude = document.querySelector("#TieudeTxt");
    // Sắp xếp theo thứ tự người dùng thêm
    const sortedInputs = Array.from(allInputs).sort((a, b) => {
        return parseInt(a.dataset.index) - parseInt(b.dataset.index);
    });

    // Đưa vào formData đúng thứ tự
    if (tieude.value.trim() === '') {
        isValid = false;
        errorMessage += "Vui lòng nhập tiêu đề ";
    
        
    }
    formData.append("tieude", tieude.value);

    sortedInputs.forEach((input, i) => {
        if (input.dataset.type === "text") {
            if (input.value.trim() === '') {
                isValid = false;
                errorMessage += "Vui lòng nhập nội dung " + (i + 1) + "\n";
            } else {
                formData.append("input" + i, "([Start" + i + "])" + input.value + "([End" + i + "])");
            }
        } else if (input.dataset.type === "file" && input.files.length > 0) {
            const file = input.files[0];
            formData.append("file" + i, file);
            formData.append("(filename" + i, "([image" + i + "])" + file.name + "([image" + i + "])");
        } else if (input.dataset.type === "file" && input.files.length === 0) {
            isValid = false;
            errorMessage += "Vui lòng chọn một file cho input " + (i + 1) + "\n";
        }
    });

    // Nếu không hợp lệ, hiển thị thông báo
    if (!isValid) {
        showAlert(errorMessage,"warning");
        return; // Dừng quá trình gửi
    }

    // Gửi dữ liệu lên server nếu hợp lệ
    fetch("/res/handle/baiviet/addBaiVietHandler.ashx", {
        method: "POST",
        body: formData
    })
        .then(res => res.text())
        .then(msg => {
            msg = msg.trim();
            if (msg === "fail") {
                showAlertAdd("Không thêm được do thiếu nội dung", "warning")
            } else if (msg === "success") {
                showAlertAdd("Cập Nhật Thành Công", "success")
                
            } else if (msg ==="exist") {
                showAlertAdd("Đã tồn tại bài viết này", "warning")
            }
        })
        .catch(err => alert("Lỗi: " + err));
}


//===============================Function====================================
function getMaxDivDataIndex() {
    const wrappers = document.querySelectorAll("div[data-index]");
    let maxIndex = -1;

    wrappers.forEach(div => {
        const index = parseInt(div.dataset.index);
        if (!isNaN(index) && index > maxIndex) {
            maxIndex = index;
        }
    });

    return maxIndex;
}

function showAlertAdd(notice, warn) {
    Swal.fire({
        title: notice,
        icon: warn,
        confirmButtonText: 'OK'
    }).then(() => {
        __doPostBack('closeForm');
    });
}


function showAlert(notice, warn) {
    Swal.fire({
        title: notice,
        icon: warn,
        confirmButtonText: 'OK'
    });
}