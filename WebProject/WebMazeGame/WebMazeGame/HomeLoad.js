// press for single and multi player games
function pressIfIn(site) {
   /* if (sessionStorage.getItem("ifUserIn") != "yes") {
        alert("You need to log in");
    } else {
        press(site);
    }*/

    press(site);
}

function pressForLogIn() {
    // if the user is in, load log out
    /*if (sessionStorage.getItem("ifUserIn") == "yes") {
        // log out
        var buttonLogIn = document.getElementById("login");
        buttonLogIn.innerHTML = "Login";
        sessionStorage.setItem("ifUserIn", "no");
        document.getElementById("register").innerHTML = "Register";
    }
    else {
        // if the user is not in, load log in
        $('#content').load('Login.html');
    }*/
}

function choosePress(site) {
    // if the user is not in load site
    //if (sessionStorage.getItem("ifUserIn") != "yes") {
        press(site);
    //} 
    // if the user is in, do nothing
}

function press(site) {
    $('#content').load(site);
};
