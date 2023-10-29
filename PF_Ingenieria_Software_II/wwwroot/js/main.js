//Nav bar effect on scroll
window.addEventListener("scroll", function(){
    const header  = document.querySelector("header");
    header.classList.toggle("sticky", window.scrollY > 0);
});

//Responsive Nav Menu
const menuBtn = document.querySelector(".nav-menu-btn");
const closeBtn = document.querySelector(".nav-close-btn");
const navigation = document.querySelector(".navigation");

menuBtn.addEventListener("click", () => {
    navigation.classList.add("active");
});

closeBtn.addEventListener("click", () => {
    navigation.classList.remove("active");
});

const sections = document.querySelectorAll("section"); // Obtener todas las secciones
const menuLinks = document.querySelectorAll('.navigation a[href^="#"]'); // Obtener todos los enlaces del menú

const observer = new IntersectionObserver((entries) => {
  entries.forEach((entry) => {
    if (entry.isIntersecting) {
      // Remover la clase 'actual' de todos los enlaces del menú
      menuLinks.forEach((link) => link.classList.remove("actual"));
      
      // Obtener el ID de la sección actualmente visible
      const id = entry.target.getAttribute("id");
      
      // Encontrar el enlace correspondiente en el menú y agregar la clase 'actual'
      const menuLink = document.querySelector(`.navigation a[href="#${id}"]`);
      if (menuLink) {
        menuLink.classList.add("actual");
      }
    }
  });
}, { rootMargin: "-30% 0px -70% 0px" });

sections.forEach((section) => {
  observer.observe(section);
});

menuLinks.forEach((menuLink) => {
  menuLink.addEventListener("click", function () {
    // Remover la clase 'actual' de todos los enlaces del menú
    menuLinks.forEach((link) => link.classList.remove("actual"));
    // Agregar la clase 'actual' al enlace seleccionado
    menuLink.classList.add("actual");

    navigation.classList.remove("active"); // Cerrar el menú después de hacer clic en un enlace
  });
});
