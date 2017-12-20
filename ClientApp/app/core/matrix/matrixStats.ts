export interface UsageScore {
    use: string;
    score: number;
}

export interface ConcreteAnswer {
    question: string;
    answer: string;
}

export interface MatrixStats {
    parcelId: number;
    bestUses: string[];
    usageScores: UsageScore[];
    concreteAnswers: ConcreteAnswer[];
}
