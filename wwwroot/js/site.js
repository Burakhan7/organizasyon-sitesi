// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// ---- Balon fiziği: scroll'a atalet tepkisi 🎈 ----
(function () {
    const balonlar = document.querySelector('.balonlar');
    if (!balonlar) return;

    let sonScroll = window.scrollY;
    let hiz = 0;          // scroll'dan gelen itki
    let kayma = 0;        // balonların mevcut sapması (px)

    window.addEventListener('scroll', function () {
        // scroll farkı = itki; aşağı inince balonlar yukarı kalır (ters yön)
        hiz += (window.scrollY - sonScroll) * -0.15;
        sonScroll = window.scrollY;
    }, { passive: true });

    function fizikDongusu() {
        // yay: mevcut sapmayı sıfıra doğru çek
        kayma += hiz;
        hiz *= 0.82;                 // sürtünme: itki her karede sönümlenir
        kayma *= 0.88;               // yay geri çekişi

        // aşırı savrulmayı sınırla
        if (kayma > 28) kayma = 28;
        if (kayma < -28) kayma = -28;

        balonlar.style.transform = 'translateY(' + kayma.toFixed(2) + 'px)';
        requestAnimationFrame(fizikDongusu);
    }

    requestAnimationFrame(fizikDongusu);
})();
// ---- Scroll'da beliren içerik ----
(function () {
    const elemanlar = document.querySelectorAll('.belir');
    if (elemanlar.length === 0) return;

    const gozlemci = new IntersectionObserver(function (girdiler) {
        girdiler.forEach(function (girdi) {
            if (girdi.isIntersecting) {
                girdi.target.classList.add('gorundu');
                gozlemci.unobserve(girdi.target);   // bir kez belirdi, artık izleme
            }
        });
    }, { threshold: 0.15 });   // elemanın %15'i görününce tetikle

    elemanlar.forEach(function (el) { gozlemci.observe(el); });
})();