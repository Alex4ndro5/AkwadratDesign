window.onload = function () {
    window.addEventListener('scroll', function (e) {
        if (window.pageYOffset > 100) {
            this.document.querySelector('header').classList.add('is-hidden');
            this.document.querySelector('.side-menu-container').classList.add('is-visible');
        }
        else {
            this.document.querySelector('header').classList.remove('is-hidden');
            this.document.querySelector('.side-menu-container').classList.remove('is-visible');
        }
    });
}
// Get all the links in the navigation menu
const navLinks = document.querySelectorAll('nav a');

// Add a click event listener to each link
navLinks.forEach(link => {
    link.addEventListener('click', event => {
        // Prevent the default link behavior
        event.preventDefault();

        // Get the target section's ID from the link's href attribute
        const targetId = link.getAttribute('href');

        // Use the target section's ID to get the corresponding element
        const targetElement = document.querySelector(targetId);

        // Calculate the distance from the top of the page to the target element
        const targetOffset = targetElement.offsetTop;

        // Use the window.scrollTo() method to smoothly scroll to the target element
        window.scrollTo({
            top: targetOffset,
            behavior: 'smooth'
        });
    });
});