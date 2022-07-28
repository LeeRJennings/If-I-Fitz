export const dateFormatter = (string) => {
    const date = new Date(string) 
    let day = date.getUTCDate() 
    let month = date.getUTCMonth() +1
    let year = date.getUTCFullYear() 
    const formattedDate = month + "/" + day + "/" + year 
    return formattedDate 
}