//===================================================================================
// Microsoft Developer & Platform Evangelism
//=================================================================================== 
// THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND, 
// EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE IMPLIED WARRANTIES 
// OF MERCHANTABILITY AND/OR FITNESS FOR A PARTICULAR PURPOSE.
//===================================================================================
// Copyright (c) Microsoft Corporation.  All Rights Reserved.
// This code is released under the terms of the MS-LPL license, 
// http://microsoftnlayerapp.codeplex.com/license
//===================================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.Samples.NLayerApp.Domain.Core.Specification;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Microsoft.Samples.NLayerApp.Domain.Core.Tests
{
    /// <summary>
    /// Summary description for SpecificationTests
    /// </summary>
    [TestClass]
    public class SpecificationTests
    {
        #region Nested Class

        public class TEntity
        {
            public int Id { get; set; }
            public string SampleProperty { get; set; }
        }

        #endregion

        [TestMethod]
        public void DirectSpecification_Constructor_Test()
        {
            //Arrange
            DirectSpecification<TEntity> adHocSpecification;
            Expression<Func<TEntity,bool>> spec = s => s.Id==0;

            //Act
            adHocSpecification = new DirectSpecification<TEntity>(spec);

            //Assert
            Assert.ReferenceEquals(new PrivateObject(adHocSpecification).GetField("_MatchingCriteria"), spec);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void DirectSpecification_Constructor_NullSpecThrowArgumentNullException_Test()
        {
            //Arrange
            DirectSpecification<TEntity> adHocSpecification;
            Expression<Func<TEntity, bool>> spec = null;

            //Act
            adHocSpecification = new DirectSpecification<TEntity>(spec);
        }
        [TestMethod()]
        public void AndSpecification_Creation_Test()
        {
            //Arrange
            DirectSpecification<TEntity> leftAdHocSpecification;
            DirectSpecification<TEntity> rightAdHocSpecification;

            Expression<Func<TEntity, bool>> leftSpec = s => s.Id == 0;
            Expression<Func<TEntity, bool>> rightSpec = s => s.SampleProperty.Length > 2;

            leftAdHocSpecification = new DirectSpecification<TEntity>(leftSpec);
            rightAdHocSpecification = new DirectSpecification<TEntity>(rightSpec);

            //Act
            AndSpecification<TEntity> composite = new AndSpecification<TEntity>(leftAdHocSpecification, rightAdHocSpecification);

            //Assert
            Assert.IsNotNull(composite.SatisfiedBy());
            Assert.ReferenceEquals(leftAdHocSpecification, composite.LeftSideSpecification);
            Assert.ReferenceEquals(rightAdHocSpecification, composite.RightSideSpecification);
        }
        [TestMethod()]
        public void OrSpecification_Creation_Test()
        {
            //Arrange
            DirectSpecification<TEntity> leftAdHocSpecification;
            DirectSpecification<TEntity> rightAdHocSpecification;

            Expression<Func<TEntity, bool>> leftSpec = s => s.Id == 0;
            Expression<Func<TEntity, bool>> rightSpec = s => s.SampleProperty.Length > 2;

            leftAdHocSpecification = new DirectSpecification<TEntity>(leftSpec);
            rightAdHocSpecification = new DirectSpecification<TEntity>(rightSpec);

            //Act
            OrSpecification<TEntity> composite = new OrSpecification<TEntity>(leftAdHocSpecification, rightAdHocSpecification);

            //Assert
            Assert.IsNotNull(composite.SatisfiedBy());
            Assert.ReferenceEquals(leftAdHocSpecification, composite.LeftSideSpecification);
            Assert.ReferenceEquals(rightAdHocSpecification, composite.RightSideSpecification);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AndSpecification_Creation_NullLeftSpecThrowArgumentNullException_Test()
        {
            //Arrange
            DirectSpecification<TEntity> leftAdHocSpecification;
            DirectSpecification<TEntity> rightAdHocSpecification;

            Expression<Func<TEntity, bool>> leftSpec =s=>s.Id==0;
            Expression<Func<TEntity, bool>> rightSpec = s => s.SampleProperty.Length > 2;

            leftAdHocSpecification = new DirectSpecification<TEntity>(leftSpec);
            rightAdHocSpecification = new DirectSpecification<TEntity>(rightSpec);

            //Act
            AndSpecification<TEntity> composite = new AndSpecification<TEntity>(null, rightAdHocSpecification);

        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AndSpecification_Creation_NullRightSpecThrowArgumentNullException_Test()
        {
            //Arrange
            DirectSpecification<TEntity> leftAdHocSpecification;
            DirectSpecification<TEntity> rightAdHocSpecification;

            Expression<Func<TEntity, bool>> rightSpec = s=>s.Id==0;
            Expression<Func<TEntity, bool>> leftSpec = s => s.SampleProperty.Length > 2;

            leftAdHocSpecification = new DirectSpecification<TEntity>(leftSpec);
            rightAdHocSpecification = new DirectSpecification<TEntity>(rightSpec);

            //Act
            AndSpecification<TEntity> composite = new AndSpecification<TEntity>(leftAdHocSpecification, null);

        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void OrSpecification_Creation_NullLeftSpecThrowArgumentNullException_Test()
        {
            //Arrange
            DirectSpecification<TEntity> leftAdHocSpecification;
            DirectSpecification<TEntity> rightAdHocSpecification;

            Expression<Func<TEntity, bool>> leftSpec = s=>s.Id==0;
            Expression<Func<TEntity, bool>> rightSpec = s => s.SampleProperty.Length > 2;

            leftAdHocSpecification = new DirectSpecification<TEntity>(leftSpec);
            rightAdHocSpecification = new DirectSpecification<TEntity>(rightSpec);

            //Act
            OrSpecification<TEntity> composite = new OrSpecification<TEntity>(null, rightAdHocSpecification);

        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void OrSpecification_Creation_NullRightSpecThrowArgumentNullException_Test()
        {
            //Arrange
            DirectSpecification<TEntity> leftAdHocSpecification;
            DirectSpecification<TEntity> rightAdHocSpecification;

            Expression<Func<TEntity, bool>> rightSpec = s=>s.Id==0;
            Expression<Func<TEntity, bool>> leftSpec = s => s.SampleProperty.Length > 2;

            leftAdHocSpecification = new DirectSpecification<TEntity>(leftSpec);
            rightAdHocSpecification = new DirectSpecification<TEntity>(rightSpec);

            //Act
            OrSpecification<TEntity> composite = new OrSpecification<TEntity>(leftAdHocSpecification, null);

        }
        [TestMethod]
        public void Specification_AndOperator_Test()
        {
            //Arrange
            DirectSpecification<TEntity> leftAdHocSpecification;
            DirectSpecification<TEntity> rightAdHocSpecification;

            Expression<Func<TEntity, bool>> leftSpec = s => s.Id == 0;
            Expression<Func<TEntity, bool>> rightSpec = s => s.SampleProperty.Length>2;

            Expression<Func<TEntity, bool>> expected = null;
            Expression<Func<TEntity, bool>> actual = null;

            //Act
            leftAdHocSpecification = new DirectSpecification<TEntity>(leftSpec);
            rightAdHocSpecification = new DirectSpecification<TEntity>(rightSpec);

            ISpecification<TEntity> andSpec = leftAdHocSpecification & rightAdHocSpecification;
            andSpec = leftAdHocSpecification || rightAdHocSpecification;
            //Assert
            

            InvocationExpression invokedExpr = Expression.Invoke(rightSpec, leftSpec.Parameters.Cast<Expression>());
            expected = Expression.Lambda<Func<TEntity, bool>>(Expression.AndAlso(leftSpec.Body, invokedExpr), leftSpec.Parameters);

            actual = andSpec.SatisfiedBy();

            
          
        }
        [TestMethod]
        public void Specification_OrOperator_Test()
        {
            //Arrange
            DirectSpecification<TEntity> leftAdHocSpecification;
            DirectSpecification<TEntity> rightAdHocSpecification;

            Expression<Func<TEntity, bool>> leftSpec = s => s.Id == 0;
            Expression<Func<TEntity, bool>> rightSpec = s => s.SampleProperty.Length > 2;

            Expression<Func<TEntity, bool>> expected = null;
            Expression<Func<TEntity, bool>> actual = null;

            //Act
            leftAdHocSpecification = new DirectSpecification<TEntity>(leftSpec);
            rightAdHocSpecification = new DirectSpecification<TEntity>(rightSpec);

            ISpecification<TEntity> orSpec = leftAdHocSpecification | rightAdHocSpecification;
            orSpec = leftAdHocSpecification || rightAdHocSpecification;

            //Assert


            InvocationExpression invokedExpr = Expression.Invoke(rightSpec, leftSpec.Parameters.Cast<Expression>());
            expected = Expression.Lambda<Func<TEntity, bool>>(Expression.Or(leftSpec.Body, invokedExpr), leftSpec.Parameters);

            actual = orSpec.SatisfiedBy();


        }
        [TestMethod()]
        public void NotSpecification_Creation_WithSpecification_Test()
        {
            //Arrange
            Expression<Func<TEntity, bool>> specificationCriteria = t => t.Id == 0;
            DirectSpecification<TEntity> specification = new DirectSpecification<TEntity>(specificationCriteria);

            //Act
            NotSpecification<TEntity> notSpec = new NotSpecification<TEntity>(specification);
            Expression<Func<TEntity,bool>> resultCriteria = new PrivateObject(notSpec).GetField("_OriginalCriteria") as Expression<Func<TEntity,bool>>;

            //Assert
            Assert.IsNotNull(notSpec);
            Assert.IsNotNull(resultCriteria);
            Assert.IsNotNull(notSpec.SatisfiedBy());
            
        }
        [TestMethod()]
        public void NotSpecification_Creation_WithCriteria_Test()
        {
            //Arrange
            Expression<Func<TEntity, bool>> specificationCriteria = t => t.Id == 0;
            

            //Act
            NotSpecification<TEntity> notSpec = new NotSpecification<TEntity>(specificationCriteria);

            //Assert
            Assert.IsNotNull(notSpec);
            Assert.IsNotNull(new PrivateObject(notSpec).GetField("_OriginalCriteria"));
        }
        [TestMethod()]
        public void NotSpecification_Creation_FromNegationOperator()
        {
            //Arrange
            Expression<Func<TEntity, bool>> specificationCriteria = t => t.Id == 0;


            //Act
            DirectSpecification<TEntity> spec = new DirectSpecification<TEntity>(specificationCriteria);
            ISpecification<TEntity> notSpec = !spec;

            //Assert
            Assert.IsNotNull(notSpec);
            
        }
        [TestMethod()]
        public void NotSpecification_Operators()
        {
            //Arrange
            Expression<Func<TEntity, bool>> specificationCriteria = t => t.Id == 0;


            //Act
            Specification<TEntity> spec = new DirectSpecification<TEntity>(specificationCriteria);
            Specification<TEntity> notSpec = !spec;
            ISpecification<TEntity> resultAnd = notSpec && spec;
            ISpecification<TEntity> resultOr = notSpec || spec;

            //Assert
            Assert.IsNotNull(notSpec);
            Assert.IsNotNull(resultAnd);
            Assert.IsNotNull(resultOr);

        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NotSpecification_Creation_NullSpecificationThrowArgumentNullException_Test()
        {
            //Arrange
            NotSpecification<TEntity> notSpec;

            //Act
            notSpec = new NotSpecification<TEntity>((ISpecification<TEntity>)null);
        }
        [TestMethod()]
        [ExpectedException(typeof(ArgumentNullException))]
        public void NotSpecification_Creation_NullCriteriaThrowArgumentNullException_Test()
        {
            //Arrange
            NotSpecification<TEntity> notSpec;

            //Act
            notSpec = new NotSpecification<TEntity>((Expression<Func<TEntity,bool>>)null);
        }
        [TestMethod()]
        public void TrueSpecification_Creation_Test()
        {
            //Arrange
            ISpecification<TEntity> trueSpec = new TrueSpecification<TEntity>();
            bool expected = true;
            bool actual = trueSpec.SatisfiedBy().Compile()(new TEntity());
            //Assert
            Assert.IsNotNull(trueSpec);
            Assert.AreEqual(expected,actual);
        }
    }
}
