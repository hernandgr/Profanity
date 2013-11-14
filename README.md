Profanity
=========
[Profanity](https://github.com/hernandgr/Profanity) is a simple library that implements a word filter. The library takes the list of words you specify, and then you can see if one text contains it or also to sanitize the text.

### Usage:

Check if text contains profanity:

	ProfanityFilter profanityFilter = new ProfanityFilter(yourOwnBlackListWords);
    bool textContainsProfanity = profanityFilter.ValidateTextContainsProfanity(text);
    
Sanitize text profanity:

	ProfanityFilter profanityFilter = new ProfanityFilter(yourOwnBlackListWords);
	string cleanText = profanityFilter.CleanTextProfanity(text);
	
Note: ProfanityFilter class constructor expects an IEnumerable collection with your blacklist of words.