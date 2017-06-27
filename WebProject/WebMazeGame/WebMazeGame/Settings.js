// if there is a click on submit button
$("#submit_btn").click(function () {
    localStorage.ip = $("#ip_text").val();
    localStorage.port = $("#port_text").val();
    localStorage.rows = $("#rows_text").val();
    localStorage.cols = $("#cols_text").val();
    localStorage.searchAlgo = $("#search_algo_text").val();
});

