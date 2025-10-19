const loader = document.getElementsByClassName("loader")[0];

const form = document.getElementById("theForm");


form.addEventListener('submit', () => {
    loader.classList.remove("hidden");
});