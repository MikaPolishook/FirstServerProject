import { send } from "../utilities";

let checks = document.querySelectorAll(".favcheckbox") as NodeListOf<HTMLInputElement>;

let userId = localStorage.getItem("userId");

for (let i = 0; i < checks.length; i++) {

    checks[i].onchange = function() {
      if (checks[i].checked) {
        send("addToFavorite", [i, userId , checks[i].checked]); 
      }
      else {
        send("removeFromFavorites", [userId, checks[i].checked]);
      }
    }
}
