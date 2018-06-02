# Importing the libraries
import numpy as np # Math library
import matplotlib.pyplot as plt #Plotting library
import pandas as pd # Import/manage dataset library

# Importing the dataset using pandas. In this case our dataset is a CVS file
dataset = pd.read_csv('Data.csv')

############## Read independant variables #################
# Now we are going to select all our independant variables. In this case this
# will be the country, age and salary. We will store them in the X variable.

# iloc - first parameter are the rows of the dataset, the second parameter
# are the columns. -1 means we don't take the last column, because the last
# column is our dependant variable.
X = dataset.iloc[:, :-1].values

############## Read dependant variables #################
# In this dataset our only dependant variable is the last column (index 3)
# which contains whether or not the customer purchased the item.  
Y = dataset.iloc[:, 3].values

############## Taking care of the missing data #################
# The age and salary columns have missing data. We are going to use the sklearn
# preprocessing library to replace the missing values with the average value. 
from sklearn.preprocessing import Imputer

# The missing values in our dataset are labeled as 'NaN'. Strategy is mean and 
# the axis is 0 (column)
imputer = Imputer(missing_values = 'NaN', strategy = "mean", axis = 0)

# Replace the missing values in our independant variables. Only select the 
# columns that have missing data (column index 1 and 2). 
imputer = imputer.fit(X[:, 1:3])

# Apply the transformation to the X variable
X[:, 1:3] = imputer.transform(X[:, 1:3]);

############## Taking care of the categorical data #################
# Categorical data is text data, such as the country and purchased columns.
# We can't do anything with these columns at the moment, because they are text.
# We need to convert them to numerical data (encode them).
from sklearn.preprocessing import LabelEncoder

# The first column we are going to transform is the country column
labelencoder_X = LabelEncoder()
X[:, 0] = labelencoder_X.fit_transform(X[:, 0])

# Because spain isn't greater then france or germany, we need to turn the
# country column into three different columns. Otherwise our model will think 
# there is an order for these countries. One for each country. For this we are
# going to use the OneHotEncoder class. 
from sklearn.preprocessing import OneHotEncoder
oneHotEncoder = OneHotEncoder(categorical_features = [0]) # country column index
X = oneHotEncoder.fit_transform(X).toarray();

# The second column we are going to transform is the purchased column
labelencoder_Y = LabelEncoder()
Y  = labelencoder_X.fit_transform(Y)

############## Splitting the dataset into a training and a test set #################
from sklearn.cross_validation import train_test_split

# We are going to use 20% of our dataset as a test set. The training set will
# be used by our model to train. When it's done, it will use the test set to
# verify if it understands the correlations between the model
X_train, X_test, Y_train, Y_test = train_test_split(X, Y, test_size = 0.20, random_state = 0)

############## Feature scaling #################
# Variables need to be on the same scale. Because the salary column contains
# much higher number then the age column, our machine learning algorithm will
# neglect the age column. Therefor, we need to put them on the same scale.
# For most libraries this won't be necessery because they do it themselves
from sklearn.preprocessing import StandardScaler
standardscaler_X = StandardScaler()
X_train = standardscaler_X.fit_transform(X_train)
X_test = standardscaler_X.transform(X_test)