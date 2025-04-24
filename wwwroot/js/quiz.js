function displayresults(){

    //variable for number of questions correct
    var numberCorrect = 0;
    //array to hold the students' choices
    var choices = [];
    //array to hold the correct answers
    var answer = [];
    //variable to hold the students' score
    var score = 0


    //get the students selections
    var result = document.getElementsByTagName('input');
    //get the correct answers
    var rightChoice = document.getElementsByName('correctAnswer');
    //get the correct answers
    var answers = document.getElementsByTagName('label');

    //add student selections to choices array
    for(i=0; i<result.length; i++){
            if (result[i].checked){
        choices.push(result[i].value);
            }
        }

    //add correct answers to the answer array
    for(k=0; k<rightChoice.length; k++){
        answer.push(rightChoice[k].innerHTML);

     }

     //calculate the number of correct answers the student has
    for( m = 0; m< choices.length; m++){
            if(answer[m] == choices[m]){
        numberCorrect = numberCorrect + 1;
            }
     }

     //calculate the student score
    score = ((numberCorrect)/choices.length)*100;

    //display correct choices and score
    document.getElementById("correct").innerHTML += "Your score is: "+score + "%";
    for(m = 0; m < answers.length; m++){
        answers[m].style.visibility = "visible";

     }

}

