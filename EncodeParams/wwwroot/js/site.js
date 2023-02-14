// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const SECRATE_KEY = 'TECHDawlance2022'
const IV = 'Fin@Dawlance2022'
const encryptPass = (pas) => {
    let key = CryptoJS.enc.Utf8.parse(SECRATE_KEY);
    let iv = CryptoJS.enc.Utf8.parse(IV)
    let encrypted = CryptoJS.AES.encrypt(pas, key, {
        iv: iv,
        mode: CryptoJS.mode.CBC
    });
    return encrypted.toString();
}
