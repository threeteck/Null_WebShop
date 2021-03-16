function getLocation(){
    if (navigator.geolocation)
        navigator.geolocation.getCurrentPosition(setCityAutomatically)
    else
        alert("Geolocation isn't supported")
}

async function setCityAutomatically(position) {
    let response = await fetch('https://api.bigdatacloud.net/data/reverse-geocode-client?latitude='
        + position.coords.latitude
        + '&longitude='
        + position.coords.longitude
        + '&localityLanguage=ru')
    if (response.ok){
        let json = await response.json()
        let city = json.city
        setCity(city)
        alert(city)//TODO do something useful with the city
    }
    else
        alert("Html error")
}

//Can be used to set city manually
function setCity(city){
    document.cookie = "city="+city
}