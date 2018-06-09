# Natural Language Processing

# Importing the libraries
import numpy as np
import matplotlib.pyplot as plt
import pandas as pd

# Importing the dataset
dataset = pd.read_csv('Restaurant_Reviews.tsv', delimiter = '\t', quoting = 3)

# Clean the dataset
import re
import nltk

# Remove all characters from the sentence that isn't a letter. 
review = re.sub('[^a-zA-Z]', ' ', dataset['Review'][0])

# Convert all words to lower characters
review = review.lower()

# Split the sentence so it will become an array of words
review = review.split()

# Download a list of stopwords so we can remove them from our dataset
# We don't want any stopwords in our dataset, because they are irrelevant
# to our machine learning algorithm
nltk.download('stopwords')
from nltk.corpus import stopwords

# Create a stemmer that will stem the words in our dataset. For example:
# 'Loved' will become 'Love'
from nltk.stem.porter import PorterStemmer
ps = PorterStemmer()

# Loop through the words, stem them and remove any stopwords
review = [ps.stem(word) for word in review if not word in set(stopwords.words('english'))]

