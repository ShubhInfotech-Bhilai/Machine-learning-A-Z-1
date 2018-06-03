# Importing the libraries
import numpy as np # Math library
import matplotlib.pyplot as plt #Plotting library
import pandas as pd # Import/manage dataset library

# Importing the dataset using pandas. In this case our dataset is a CVS file
dataset = pd.read_csv('Position_Salaries.csv')

############## Read independant variables #################
# Now we are going to select all our independant variables. In this case our
# independent variables are position and level

# iloc - first parameter are the rows of the dataset, the second parameter
# are the columns. -1 means we don't take the last column, because the last
# column is our dependant variable.
X = dataset.iloc[:, 1:2].values

############## Read dependant variables #################
# In this dataset our only dependant variable is the second column
# which contains the salary
Y = dataset.iloc[:, 2].values

############## Fitting linear regression to dataset #################
from sklearn.linear_model import LinearRegression
regression_linear = LinearRegression()
regression_linear.fit(X, Y)

############## Fitting polynomial regression to dataset #################
from sklearn.preprocessing import PolynomialFeatures
polynomial_features = PolynomialFeatures(degree = 4)

# This line will create a new vector containing the original value + a value
# with the power of the original value
X_polynomial = polynomial_features.fit_transform(X)

# perform linear regression on the polynomial model
regression_polynomial = LinearRegression()
regression_polynomial.fit(X_polynomial, Y)

############## Visualize the results #################
plt.scatter(X, Y, color = 'red')
plt.plot(X, regression_linear.predict(X), color = 'blue')
plt.plot(X, regression_polynomial.predict(X_polynomial), color = 'green')
plt.title('Salary vs position')
plt.xlabel('Position level')
plt.ylabel('Salary')
plt.show()