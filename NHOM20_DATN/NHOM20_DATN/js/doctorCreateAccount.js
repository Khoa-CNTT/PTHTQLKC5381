

var btn_regist = document.querySelector('.btn_regist_dt');
var btn_list = document.querySelector('.btn_list');
var regist_form = document.querySelector('#regist_here');
var list_doctor = document.querySelector('#list_here');

///// choose div 
btn_regist.addEventListener("click", function () {
    btn_regist.style.background = 'darkorange';
    btn_list.style.background = '';
    regist_form.style.display = 'block';
    list_doctor.style.display = 'none';
    //document.querySelector('.btn_regist_dt').style.background = 'darkorange';
    //document.querySelector('.btn_list').style.background = '';
});


btn_list.addEventListener("click", function () {
    btn_list.style.background = 'darkorange';
    btn_regist.style.background = '';
    list_doctor.style.display = 'block';
    regist_form.style.display = 'none';




    //document.querySelector('.btn_list').style.background = 'darkorange';
    //document.querySelector('.btn_regist_dt').style.background = '';
});
