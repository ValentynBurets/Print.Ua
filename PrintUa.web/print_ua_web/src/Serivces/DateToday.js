
const DateToday = () => {    
    var DateToday = new Date()
    var dd = String(DateToday.getDate()).padStart(2, '0')
    var mm = String(DateToday.getMonth() + 1).padStart(2, '0') //January is 0!
    var yyyy = DateToday.getFullYear()
    DateToday = mm + '/' + dd + '/' + yyyy
    return DateToday;
};

export default DateToday