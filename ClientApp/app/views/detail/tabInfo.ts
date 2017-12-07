
export class TabInfo {
    public question: string;
    private _answer: boolean;
    get answer() { return this._answer ? "Yes" : "No"; }

    constructor(question: string, answer: boolean) {
        this.question = question;
        this._answer = answer;
    }
}
