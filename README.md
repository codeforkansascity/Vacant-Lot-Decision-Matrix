# Vacant-Lot-Decision-Matrix

### Stakeholder:
* KCMO Neighborhoods & Housing Services Department
* UNI

### Problem Statement 
The city has a large inventory of vacant lots but does not have a strategy to sell those lots. When opportunities arise to sell these lots, the city relies on the “eye test” of staff to determine if the proposed use for a lot is truly the “best” use. This tool would help the city and neighborhood stakeholders to identify the best use for vacant lots. Based on this information, the city, along with interested stakeholders, can better strategize for the repurposing of these vacant lots.

### Minimum Viable Solution
A website that provides information about vacant lots to better identify what their best use. 
A target use will be determined based on a decision matrix (series of 8-10 questions). 
The website would work as follows: user would search a property by KIVA or address. 
They would see the “yes” or “no” answers to those questions, as well as the best use based on those answers. 

Decision Matrix Questions |	Data Available? |	Data Source?
-------------------------- | ---------------- | ------------------
Area Conditions	|	
What is the MVA score? |	Yes | 	OpenDataKc
Is there a park within a half-mile?	| Yes	| KCMO Parcel Viewer
What is the population density of the area?	| Yes	| Census
Property Features	|	
What is the size of the parcel?	| Yes	| KCMO Parcel Viewer
Is the parcel adjacent to another vacant lot?	| Yes	| Spatial analysis based on city data
Is the parcel adjacent to a residential property?	| Yes	| Spatial analysis based on city data
Does the parcel provide a link to nature corridor?	| Maybe	| Parks or Heartland Conservation Alliance
Site Hindrances		 |
Is there known soil contamination in area?	| Yes	| Missouri DNR
Is there a foundation of a demolished home buried underground?	| Maybe	| KCMO Dangerous Buildings
Is the site in a known floodplain?	| Yes	| KCMO Parcel Viewer
