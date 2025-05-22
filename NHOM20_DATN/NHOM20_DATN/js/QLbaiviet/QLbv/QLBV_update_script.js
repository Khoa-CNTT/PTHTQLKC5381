let previousFiles = {};
//function loadDataFromServer() {
//    fetch("updateTest.ashx")
//        .then(res => res.json())
//        .then(dataList => {
//            renderSavedContent(dataList.id, dataList.tieude, dataList.content)
//        }).catch(err => console.error("Lỗi khi fetch:", err));
//}

function checkAddOrUpdate() {
    const container = document.getElementById("dynamicInputs");
    const idBV = document.getElementById("idHidden");
    if (container.contains(idBV)) {
        //update
        updateDBBaiViet(idBV.textContent);
    } else {
        //add 
        addBV();
    }
}



function loadDataFromServer(id) {
    fetch("/res/handle/baiviet/LoadUpdateBaiVietHandler.ashx?mode=edit&id="+id+"")
        .then(res => res.json() )
        .then(dataList => {
            renderSavedContent(dataList.idBV, dataList.tieude, dataList.content)
        }).catch(err => console.error("Lỗi khi fetch:", err));
}


function updateDBBaiViet(id) {
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

    //check tieu de  phai nhap
    if (tieude.value.trim() === '') {
        isValid = false;
        errorMessage += "Vui lòng nhập tiêu đề ";


    }
   
    // Đưa vào formData đúng thứ tự
    sortedInputs.forEach((input, i) => {
        const fileNameSpan = document.querySelector("#hiddenImage" + i);
        if (input.dataset.type === "text") {
            if (input.value.trim() === '') {
                isValid = false;
                errorMessage += "Vui lòng nhập nội dung thứ " + (i + 1) + "\n";
            } else {
                formData.append("input" + i, "([Start" + i + "])" + input.value + "([End" + i + "])");
            }
        } else if (input.dataset.type === "file" && input.files.length > 0) {
            const file = input.files[0];
            const sizeInBytes = file.size;
            const sizeInKB = (sizeInBytes / 1024).toFixed(2);
            const sizeInMB = (sizeInBytes / (1024 * 1024)).toFixed(2);
            if (sizeInBytes > 5 * 1024 * 1024) {
                isValid = false;
                errorMessage += "Vui lòng chọn file nhỏ hơn 5MB.\n";
            }
            formData.append("file" + i, file);
            formData.append("filename" + i, "([image" + i + "])" + file.name + "([image" + i + "])");
        } else if (input.dataset.type === "file" && input.files.length === 0 && !(fileNameSpan === 0)) {
            // set file name
           
            if (fileNameSpan) {
                const fileName = fileNameSpan.textContent.trim();
                formData.append("filename" + i, "([image" + i + "])" + fileName + "([image" + i + "])")
            }

        }
    });
    formData.append("id", id);
    formData.append("tieude", tieude.value);
    formData.append("inputCount", sortedInputs.length);
    // Nếu không hợp lệ, hiển thị thông báo
    if (!isValid) {
        showAlert(errorMessage, "warning");
        return; // Dừng quá trình gửi
    }




    fetch("/res/handle/baiviet/updateBaiVietHandler.ashx", {
        method: "POST",
        body: formData
    })
        .then(res => res.text())
        .then(msg => {
            msg = msg.trim();
            
            if (msg === "fail") {
                showAlert("Không thêm được do thiếu nội dung", "warning")
            } else if (msg === "success") {
                showAlertAdd("Cập Nhật Thành Công", "success")

            } else if (msg === "exist") {
                showAlert("Đã tồn tại bài viết này", "warning")
            }
        })
        .catch(err => alert("Lỗi: " + err));
}








//==========FUnction =========
function renderSavedContent(idBV,titleValue, savedString) {
    const container = document.getElementById("dynamicInputs");
    const idHidden = document.createElement("span");
    idHidden.id = "idHidden";
    idHidden.style.display = "none";
    idHidden.textContent = idBV;
    const title = document.getElementById("TieudeTxt");
    title.value = titleValue
    container.innerHTML = "";
    container.appendChild(idHidden);
    const regex = /\(\[Start(\d+)\]\)([\s\S]*?)\(\[End\1\]\)|\(\[image(\d+)\]\)([\s\S]*?)\(\[image\3\]\)/g;
    let match;
    while ((match = regex.exec(savedString)) !== null) {
        const textIndex = match[1];       // Chỉ có nếu là text
        const textContent = match[2];     // Nội dung text
        const imageIndex = match[3];      // Chỉ có nếu là image
        const imageFilename = match[4];   // Tên file hoặc nội dung ảnh
        //console.log(textContent)
        if (textIndex !== undefined) {
            const textarea = document.createElement("textarea");
            const wrapper = document.createElement("div");
            wrapper.setAttribute("data-index", textIndex);
            wrapper.classList.add("form-group-dynamic");

            textarea.name = "dynamicInput";
            textarea.setAttribute("data-type", "text");
            textarea.setAttribute("data-index", textIndex);
            textarea.classList.add("form-control");
            textarea.rows = 3;
            textarea.cols = 40;
            textarea.value = textContent;

            const deleteBtn = document.createElement("button");
          
            deleteBtn.type = "button";
            deleteBtn.style.marginLeft = "8px";
            deleteBtn.classList.add("btn-delete-dynamic");
            deleteBtn.onclick = () => wrapper.remove();
            
           
            deleteBtn.appendChild(iconMinus());
            wrapper.appendChild(textarea);
            wrapper.appendChild(deleteBtn);
            container.appendChild(wrapper);
        }

        if (imageIndex !== undefined) {

            const wrapper = document.createElement("div");
            wrapper.setAttribute("data-index", imageIndex);
            wrapper.classList.add("form-group-dynamic-img");
            const label = document.createElement("label");
            label.textContent = `File đã tải lên: ${imageFilename}`;

            const input = document.createElement("input");
            input.type = "file";
            input.name = "dynamicInput";
            input.id = "fileInput";
            input.setAttribute("data-type", "file");
            input.setAttribute("data-index", imageIndex);
        
            const hidden = document.createElement("span");
            hidden.id = "hiddenImage"+imageIndex;
            hidden.style.display="none"
            hidden.textContent = imageFilename;
            
            const deleteBtn = document.createElement("button");
            deleteBtn.type = "button";
            deleteBtn.style.marginLeft = "8px";
            deleteBtn.classList.add("btn-delete-dynamic");
            deleteBtn.onclick = () => {
                wrapper.remove();
            };
            input.addEventListener("change", (e) => {
                const file = e.target.files[0];

                if (file) {
                    previousFiles[imageIndex] = file;
                    label.textContent = "File đã tải lên:"+file.name;
                } else {
                    // Không chọn file mới → giữ file cũ nếu có
                    if (previousFiles[imageIndex]) {
                        label.textContent ="File đã tải lên:"+  imageFilename;
                    } else {
                        label.textContent = "Chưa chọn file";
                    }
                }
            });
        
           deleteBtn.appendChild(iconMinus())
            wrapper.appendChild(input);
            wrapper.appendChild(hidden);
            wrapper.appendChild(document.createElement("br"));
            wrapper.appendChild(label);
            wrapper.appendChild(deleteBtn);
        
            container.appendChild(wrapper);
        }
    }
}

function iconMinus() {
    const iconMinus = document.createElement("i");
    iconMinus.classList.add("fa-solid");
    iconMinus.classList.add("fa-minus");
    return iconMinus;
}

document.getElementById("fileInput").addEventListener("change", function () {
    const file = this.files[0];
    const maxSize = 5 * 1024 * 1024; // 5MB

    if (file && file.size > maxSize) {
        showAlert("Vui lòng chọn file nhỏ hơn 5MB.", "warning")
        this.value = "";
    }
});