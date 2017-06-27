onload();

// on load
function onload() {
    $.post("api/Users/CreateUserRankingTable")
        .done(function (data) {
            // sort the data
            data.sort(function (first, second) {
                return (second.Wins - second.Losses)-(first.Wins - first.Losses);
            });
            var index = "1";
            // for each item in the list
            data.forEach(function (item) {
                $("<tr><td> " + index + " </td ><td> " + item.Name + " </td> <td>" + item.Wins + "</td> <td>" + item.Losses + "</td> </tr >")
                    .appendTo("#table_rankings");
            index++;
            })
        });
}