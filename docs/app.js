const playerScoreEl = document.getElementById('playerScore');
const computerScoreEl = document.getElementById('computerScore');
const resultTextEl = document.getElementById('resultText');
const flavorTextEl = document.getElementById('flavorText');
const playerMoveEl = document.getElementById('playerMove');
const computerMoveEl = document.getElementById('computerMove');
const statusNoteEl = document.getElementById('statusNote');
const resultPanelEl = document.getElementById('resultPanel');
const resetButton = document.getElementById('resetButton');
const choiceButtons = [...document.querySelectorAll('.choice')];

const choices = ['ROCK', 'PAPER', 'SCISSORS'];
const emoji = {
  ROCK: '🪨',
  PAPER: '📄',
  SCISSORS: '✂️'
};

let playerScore = 0;
let computerScore = 0;

function computerChoice() {
  return choices[Math.floor(Math.random() * choices.length)];
}

function roundResult(player, cpu) {
  if (player === cpu) {
    return 0;
  }

  const playerWins = {
    ROCK: 'SCISSORS',
    PAPER: 'ROCK',
    SCISSORS: 'PAPER'
  };

  return playerWins[player] === cpu ? 1 : -1;
}

function roundFlavor(player, cpu, result) {
  if (result === 0) {
    return 'Same move, same energy. Run it back.';
  }

  if (result > 0) {
    return `${emoji[player]} ${player.toLowerCase()} beats ${cpu.toLowerCase()}. Nice.`;
  }

  return `${emoji[cpu]} ${cpu.toLowerCase()} beats ${player.toLowerCase()}. The CPU landed it.`;
}

function updateUI() {
  playerScoreEl.textContent = playerScore;
  computerScoreEl.textContent = computerScore;
}

function setLocked(locked) {
  choiceButtons.forEach((button) => {
    button.classList.toggle('disabled', locked);
  });
}

function endMatch() {
  const playerWon = playerScore >= 3;
  resultTextEl.textContent = playerWon ? 'You won the match!' : 'Computer won the match!';
  resultTextEl.className = playerWon ? 'win' : 'lose';
  flavorTextEl.textContent = playerWon ? 'Victory dance unlocked.' : 'The CPU claimed the crown.';
  statusNoteEl.textContent = 'Press New Match to play again.';
  setLocked(true);
}

function playRound(player) {
  if (playerScore >= 3 || computerScore >= 3) {
    return;
  }

  const cpu = computerChoice();
  const result = roundResult(player, cpu);

  playerMoveEl.textContent = `${emoji[player]} ${player.toLowerCase()}`;
  computerMoveEl.textContent = `${emoji[cpu]} ${cpu.toLowerCase()}`;

  resultPanelEl.classList.remove('win', 'lose', 'draw');

  if (result === 0) {
    resultTextEl.textContent = 'It\'s a draw!';
    resultTextEl.className = 'draw';
  } else if (result > 0) {
    playerScore += 1;
    resultTextEl.textContent = 'You win the round!';
    resultTextEl.className = 'win';
  } else {
    computerScore += 1;
    resultTextEl.textContent = 'Computer wins the round!';
    resultTextEl.className = 'lose';
  }

  flavorTextEl.textContent = roundFlavor(player, cpu, result);
  statusNoteEl.textContent = 'First to 3 points wins the match.';
  updateUI();

  if (playerScore >= 3 || computerScore >= 3) {
    endMatch();
  }
}

function resetMatch() {
  playerScore = 0;
  computerScore = 0;

  updateUI();
  playerMoveEl.textContent = '-';
  computerMoveEl.textContent = '-';
  resultTextEl.textContent = 'Choose your move';
  resultTextEl.className = '';
  flavorTextEl.textContent = 'First to 3 points wins the match.';
  statusNoteEl.textContent = 'Match in progress.';
  setLocked(false);
}

choiceButtons.forEach((button) => {
  button.addEventListener('click', () => playRound(button.dataset.choice));
});

resetButton.addEventListener('click', resetMatch);

resetMatch();