
const encodeFile = (file, setFileString) => {
    var reader = new FileReader();
     //console.log(file)
    if (file) {
        reader.readAsDataURL(file);
        reader.onload = () => {
            let dataURI = reader.result;
            
            let encodedFile = split(dataURI)
            //console.log(encodedFile);
            setFileString(encodedFile);
        };
        reader.onerror = (error) => {
            console.log("error: ", error);
        };
    }
};

const split = (string) =>{
    const words = string.split('base64');
    //console.log(words[1]);
    return words[1].substring(1);
}

export default encodeFile