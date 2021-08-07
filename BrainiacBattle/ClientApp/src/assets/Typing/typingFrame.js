    let TIME_LIMIT = 60;

    // define quotes to be used
    let hasrun = 0;
    do {
      switch (Math.floor(Math.random() * 4)) {
        case 0:
          arrayOfQuote = [
            "Push yourself, because no one else is going to do it for you.",
            "Failure is the condiment that gives success its flavor.",
            "Wake up with determination. Go to bed with satisfaction.",
            "It's going to be hard, but hard does not mean impossible.",
            "Learning never exhausts the mind.",
            "The only way to do great work is to love what you do."
          ];
          hasrun = 1;
          break;
        case 1:
          arrayOfQuote = [
            "The University of North Carolina at Charlotte is a",
            "University in Charlotte North Carolina. There are ",
            "Thirty Thousand students currently attending and there are",
            "Many different sports and clubs around campus. These include",
            "Football, Basketball, Soccer, Baseball, and Volleyball",
            "The school was opened in 1946 and is one of the largest in the State of North Carolina."
          ];
          hasrun = 1;
          break;
        case 2:
          arrayOfQuote = [
            "this, different, get, second, life, story, begin, use,",
            "eat, I, because, group, most, point, and, story, fall,",
            "with, last, start, far, number, eye, should, best",
            "took, the, over, river, mean, even, animal, paper,",
            "come, red, around, state, country, begin, best, water,",
            "while, boy, above, near, earth, miss, down, first, few,",
            "along, do, who, car, mother, need, it, turn, an, his,",
            "when, point, know, does, where, my, far, of, father,",
            "sea, again, example, list, think, new, about, want,",
            "mother, will, he, also"
          ];
          hasrun = 1;
          break;
        default:
          arrayOfQuote = [
            "Brainiac Battle is a platform for multiple brain training mini-games.",
            "This application is built so you can improve your skills",
            "The categories of these games include Strategy, Reflexes, Focus",
            "Memory, Meditation, and Typing. You are currently playing the typing game",
            "Chess will be our strategy game. As well as a matching game",
            "Meditation is a playlist of videos, and Reflexes is a Whack a mole game."
          ];
          hasrun = 1
          break;
      }
    } while (hasrun = 0)



    // selecting required elements
    let timer_text = document.querySelector(".curr_time");
    let accuracy_text = document.querySelector(".curr_accuracy");
    let error_text = document.querySelector(".curr_errors");
    let cpm_text = document.querySelector(".curr_cpm");
    let wpm_text = document.querySelector(".curr_wpm");
    let quote_text = document.querySelector(".quote");
    let input_area = document.querySelector(".input_area");
    let restart_btn = document.querySelector(".restart_btn");
    let cpm_group = document.querySelector(".cpm");
    let wpm_group = document.querySelector(".wpm");
    let error_group = document.querySelector(".errors");
    let accuracy_group = document.querySelector(".accuracy");

    let timeLeft = TIME_LIMIT;
    let timeElapsed = 0;
    let total_errors = 0;
    let errors = 0;
    let accuracy = 0;
    let characterTyped = 0;
    let current_quote = "";
    let quoteNo = 0;
    let timer = null;
  



function updateQuote() {
    quote_text.textContent = null;
    current_quote = arrayOfQuote[quoteNo];

    // separate each character and make an element
    // out of each of them to individually style them
    current_quote.split('').forEach(char => {
      const charSpan = document.createElement('span')
      charSpan.innerText = char
      quote_text.appendChild(charSpan)
    })

    // roll over to the first quote
    if (quoteNo < arrayOfQuote.length - 1) {
      quoteNo++;
    }

    else {
      quoteNo = 0;
    }

  }

  function processCurrentText() {

    // get current input text and split it
    curr_input = input_area.value;
    curr_input_array = curr_input.split('');

    // increment total characters typed
    characterTyped++;

    errors = 0;

    quoteSpanArray = quote_text.querySelectorAll('span');
    quoteSpanArray.forEach((char, index) => {
      let typedChar = curr_input_array[index]

      // character not currently typed
      if (typedChar == null) {
        char.classList.remove('correct_char');
        char.classList.remove('incorrect_char');

        // correct character
      } else if (typedChar === char.innerText) {
        char.classList.add('correct_char');
        char.classList.remove('incorrect_char');

        // incorrect character
      } else {
        char.classList.add('incorrect_char');
        char.classList.remove('correct_char');

        // increment number of errors
        errors++;
      }
    });

    // display the number of errors
    error_text.textContent = total_errors + errors;

    // update accuracy text
    let correctCharacters = (characterTyped - (total_errors + errors));
    let accuracyVal = ((correctCharacters / characterTyped) * 100);
    accuracy_text.textContent = Math.round(accuracyVal);

    // if current text is completely typed
    // irrespective of errors
    if (curr_input.length == current_quote.length) {
      updateQuote();

      // update total errors
      total_errors += errors;

      // clear the input area
      input_area.value = "";
    }
  }

  function startGame() {

    resetValues();
    updateQuote();

    // clear old and start a new timer
    clearInterval(timer);
    timer = setInterval(updateTimer, 1000);
  }

  function resetValues() {
    timeLeft = TIME_LIMIT;
    timeElapsed = 0;
    errors = 0;
    total_errors = 0;
    accuracy = 0;
    characterTyped = 0;
    quoteNo = 0;
    input_area.disabled = false;

    input_area.value = "";
    quote_text.textContent = 'Click on the area below to start the game.';
    accuracy_text.textContent = 100;
    timer_text.textContent = timeLeft + 's';
    error_text.textContent = 0;
    restart_btn.style.display = "none";
    cpm_group.style.display = "none";
    wpm_group.style.display = "none";
  }

  function updateTimer() {
    if (timeLeft > 0) {
      // decrease the current time left
      timeLeft--;

      // increase the time elapsed
      timeElapsed++;

      // update the timer text
      timer_text.textContent = timeLeft + "s";
    }
    else {
      // finish the game
      finishGame();
    }
  }

  function finishGame() {
    // stop the timer
    clearInterval(timer);

    // disable the input area
    input_area.disabled = true;

    // show finishing text
    quote_text.textContent = "Click on restart to start a new game.";

    // display restart button
    restart_btn.style.display = "block";

    // calculate cpm and wpm
    cpm = Math.round(((characterTyped / timeElapsed) * 60));
    wpm = Math.round((((characterTyped / 5) / timeElapsed) * 60));
    //score = Math.round(((wpm / cpm) * (accuracy * .01)) * 10000)

    // update cpm and wpm text
    //score_text.textContent = score;
    cpm_text.textContent = cpm;
    wpm_text.textContent = wpm;



    // display the cpm and wpm
    cpm_group.style.display = "block";
    wpm_group.style.display = "block";
    //score_group.style.display = "block";
  }





