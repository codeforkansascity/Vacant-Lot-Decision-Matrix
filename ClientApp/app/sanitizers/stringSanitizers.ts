
export class StringExtensions {
    /*
    * Removes all NON-NUMBER and NON-LETTER characters from an address string
    * @param term the string.
    */
    public static sanitizeAddress(val: string) {
        const regex = /[^ A-Za-z0-9]/g;

        if (regex.test(val)) {
            val = val.replace(regex, "");
        } 

        if (val.length < 1) { 
            val = "none";
        }

        return val;
    }
}
